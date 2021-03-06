classdef splineHelper
    methods(Static)
        function [flag] = inRegion(x, region)
            if x < region(1) || x > region(1)
                flag = false;
            else
                flag = true;
            end
        end
        
        function [s] = size(region)
            s = region(2) - region(1);
        end
        
        function [hugeChange, aa, bb, cc, dd] = leftPars(x, a, b, c, stationaryPoint, flat, xEnd, lb, ub, lbmin, ubmax, tailConcavity, dx, dxx, concave)%, dontCareWingShape)
            hugeChange = false;
            if ~flat && ~isempty(lb) && (b(1) > 0) %a(1) < aMin(end) &&all
                err = 'Left needs more data';
                error('%s', err);
            end
            
            triedextended = false;
            success = false;
            while (~success)
                try
                    xEnd = xEnd(ub ~= 0);
                    lb = lb(ub ~= 0);
                    ub = ub(ub ~= 0);
                    t = x(1);
                    aa = a(1);
                    hleft = stationaryPoint - t;
                    h2 = hleft * hleft;
                    if ~isempty(xEnd)
                        delta = xEnd - t;
                        delta2 = delta .^ 2;
                        delta3 = delta .^ 3;
                        
                        alpha = 3 * h2 * (lb - aa);
                        beta = 3 * h2 * (ub - aa);
                        
                        % 3 * delta ^2 * h ^2 - 2 * delta ^ 3 * h
                        tao = 3 * delta2 * h2 - 2 * delta3 * hleft;
                        gammaa = 3 * delta * h2 - delta3;
                        
                        %             aBoundMax = (3 * h3 * gamma .* aMax - (3 * h3 * gamma - 3 * h2 * tao) .* aMin) ./ (3 * h2 .*tao);
                        %             aBoundMax = min(aBoundMax);
                        %             if a > aBoundMax
                        %                 err = 'Fail to interpolate with given boundary';
                        %                 error('error in quadprog: %s', err);
                        %             end
                        left = max(beta * hleft ./ (gammaa * hleft - tao));
                        right = min(alpha ./ gammaa);
                        
                        if (left > 0)
                            err = 'Left fail to extrapolate with given boundary';
                            error('%s', err);
                        end
                        if (left > right)
                            err = 'Left fail to extrapolate with given boundary';
                            error('%s', err);
                        end
                    end
                    
                    if flat
                        bb = 0;
                    else
                        if isnan(dx)
                            % use the weighted last few b's to calculate extrapolating b
                            xtmp = x(1: min(length(x),3));
                            w = exp(-(t-xtmp)./(t-xtmp(end)) * 3);
                            %w = exp(t-x);
                            btmp = b(1: min(length(b),3));
                            bb = w * btmp / sum(w);
                            if (sign(bb) ~= sign(b(1)) && b(1) < 0)
                                bb = b(1);
                            end
                            % bb = b(1);
                        else
                            bb = dx;
                        end
                    end
                    originb = bb;
                    if ~isempty(ub)
                        if concave && bb < left && right < 0
                            bb = right;
                        else
                            bb = min(max(bb, left), right);
                        end
                    else
                        right = inf;
                        left = -inf;
                    end
                    bb = min(0, bb); % left wing should be decreasing
                    if ~isnan(tailConcavity) && bb ~=0
                        re = bb * 0.85 + bb * 0.15 * tailConcavity;
                        if (re < 0 && re < right && re > left)
                            bb = re;
                        end
                    end
                    
                    if ((abs(originb) < 5e-4                        && (abs(bb) > 10 * abs(originb))) ...
                     || (abs(originb) > 5e-4 && abs(originb) < 1e-3 && (abs(bb) > 2.5 * abs(originb) || abs(bb) < abs(originb)/2.5)) ...
                     || (abs(originb) > 1e-3                        && (abs(bb) > 1.5 * abs(originb) || abs(bb) < abs(originb)/1.5)))
                        if ~flat && bb ~= 0
                            %                     if (bb > originb)
                            %                         aa = -inf;
                            %                         cc = -inf;
                            %                         dd = -inf;
                            %                         return;
                            %                     else
%                             err = 'Extrapolate left will change slope a lot';
%                             error('%s', err);
                            %                     end
                            hugeChange = true;
                        end
                    end
                    
                    if flat
                        cc = 0;
                    else
                        if isnan(dxx)
                            ctmp = c(2:min(length(c),3));
                            cc = - abs(w(2:2+length(ctmp)-1) * ctmp / sum(w(2:end)));
                            if sign(cc) ~= sign(c(2))
                                cc = c(2)/2;
                            end
                        else
                            cc = dxx;
                        end
                    end
                    %cc = -abs(c(2));
                    
                    maxCC = 0;
                    minCC = -bb/hleft;
                    
                    if ~isempty(ub)
                        ccBoundMax = min((beta - bb * gammaa) ./ tao);
                        if (ccBoundMax == -inf)
                            ccBoundMax = inf;
                        end
                        ccBoundMin = max((alpha - bb * gammaa) ./ tao);
                        if (ccBoundMax < ccBoundMin)
                            if (isnan(left) || bb == 0)
                                err = 'Left fail to extrapolate with given boundary';
                                error('%s', err);
                            end
                            findb = false;
                            for ii = linspace(min(0,right), left)
                                ccBoundMax = min((beta - ii * gammaa) ./ tao);
                                ccBoundMin = max((alpha - ii * gammaa) ./ tao);
                                if ccBoundMax >= ccBoundMin
                                    bb = ii;
                                    findb = true;
                                    if ((abs(originb) < 5e-4                        && (abs(bb) > 10 * abs(originb))) ...
                                     || (abs(originb) > 5e-4 && abs(originb) < 1e-3 && (abs(bb) > 2.5 * abs(originb) || abs(bb) < abs(originb)/2.5)) ...
                                     || (abs(originb) > 1e-3                        && (abs(bb) > 1.5 * abs(originb) || abs(bb) < abs(originb)/1.5)))
                                        if ~flat && bb ~= 0
                                            hugeChange = true;
                                        end
                                    end
                                    break;
                                end
                            end
                            if (~findb)
                                err = 'Left fail to extrapolate with given boundary';
                                error('%s', err);
                            end
                            %                     if (~dontCareWingShape && (abs(bb) > 3 * abs(originb) || abs(bb) < 0.3 * abs(originb)))
                            %                         if bb ~= 0
                            %                             err = 'Extrapolate left will change slope a lot';
                            %                             error('%s', err);
                            %                         end
                            %                     end
                        end
                        maxCC = min(maxCC, ccBoundMax);
                        minCC = max(minCC, ccBoundMin);
                    end
                    if concave && cc > maxCC
                        cc = minCC;
                    else
                        cc = min(max(cc, minCC), maxCC);
                        if ~isnan(tailConcavity)
                            cc = minCC + tailConcavity * (cc - minCC);
                        end
                    end
                    
                    dd = (-bb - 2 * cc * hleft) / 3 / h2;
                    
                    success = true;
                catch error1
                    if (triedextended)
                        throw(error1);
                    else
                        lb = lbmin;
                        ub = ubmax;
                        triedextended = true;
                    end
                end
            end
        end
        
        function [hugeChange, aa, bb, cc, dd] = rightPars(x, a, b, c, stationaryPoint, flat, xEnd, lb, ub, lbmin, ubmax, tailConcavity, dx, dxx, concave)%, dontCareWingShape)
            hugeChange = false;
            if ~flat && ~isempty(lb) &&  b(end) < 0 %a(end) < aMin(1) &&
                err = 'Right needs more data';
                error('%s', err);
            end
              
            triedextended = false;
            success = false;
            while (~success)
                try
                    xEnd = xEnd(ub ~= 0);
                    lb = lb(ub ~= 0);
                    ub = ub(ub ~= 0);
                    aa = a(end);
                    t = x(end);
                    hright = stationaryPoint - t;
                    h2 = hright * hright;
                    if ~isempty(xEnd)
                        delta = xEnd - t;
                        delta2 = delta .^ 2;
                        delta3 = delta .^ 3;
                        
                        alpha = 3 * h2 * (lb - aa);
                        beta = 3 * h2 * (ub - aa);
                        
                        tao = 3 * delta2 * h2 - 2 * delta3 * hright;
                        gammaa = 3 * delta * h2 - delta3;
                        
                        left = max(alpha ./ gammaa);
                        right = min(beta * hright ./ (gammaa * hright - tao));
                        
                        if (right < 0)
                            err = 'Right fail to extrapolate with given boundary';
                            error('%s', err);
                        end
                        if (left > right)
                            err = 'Right fail to extrapolate with given boundary';
                            error('%s', err);
                        end
                    end
                    
                    if flat
                        bb = 0;
                    else
                        if isnan(dx)
                            % use the weighted last few b's to calculate extrapolating b
                            xtmp = x(length(x) - min(length(x),3) + 1 : end);
                            w = exp(-(xtmp-t)./(xtmp(1) - t) * 3);
                            %w = exp(t-x);
                            btmp = b(length(b) - min(length(b),3) + 1 : end);
                            bb = w * btmp / sum(w);
                            if (sign(bb) ~= b(end) && b(end) > 0)
                                bb = b(end);
                            end
                        else
                            bb = dx;
                        end
                    end
                    originb = bb;
                    %bb = b(end);
                    if ~isempty(ub)
                        if concave && bb > right && left > 0
                            bb = left;
                        else
                            bb = min(max(bb, left), right);
                        end
                    else
                        right = inf;
                        left = -inf;
                    end
                    bb = max(bb, 0); %right wing should be increasing
                    if ~isnan(tailConcavity) && bb ~= 0
                        re = bb * 0.85 + bb * 0.15 * tailConcavity;
                        if (re < right && re > left && re > 0)
                            bb = re;
                        end
                    end
                    if ((abs(originb) < 5e-4                        && (abs(bb) > 10 * abs(originb))) ...
                     || (abs(originb) > 5e-4 && abs(originb) < 1e-3 && (abs(bb) > 2.5 * abs(originb) || abs(bb) < abs(originb)/2.5)) ...
                     || (abs(originb) > 1e-3                        && (abs(bb) > 1.5 * abs(originb) || abs(bb) < abs(originb)/1.5)))
                        if ~flat && bb ~= 0
                            %                                         err = 'Extrapolate right will change slope a lot';
                            %                                         error('%s', err);
                            hugeChange = true;
                        end
                    end
                    
                    if flat
                        cc = 0;
                    else
                        if isnan(dxx)
                            ctmp = c(length(c) - min(length(c),3) + 1: end-1);
                            cc = - abs(w(end-1-length(ctmp)+1:end-1) * ctmp / sum(w(1:end-1)));
                            %cc = -abs(c(end-1));
                            if (sign(cc) ~= sign(c(end-1)))
                                cc = c(end-1)/2;
                            end
                        else
                            cc = dxx;
                        end
                    end
                    
                    maxCC = 0;
                    minCC = -bb/hright;
                    
                    if ~isempty(ub)
                        ccBoundMax = min((beta - bb * gammaa) ./ tao);
                        if (ccBoundMax == -inf)
                            ccBoundMax = inf;
                        end
                        ccBoundMin = max((alpha - bb * gammaa) ./ tao);
                        if (ccBoundMax < ccBoundMin)
                            if (isnan(right) || bb == 0)
                                err = 'Right fail to extrapolate with given boundary';
                                error('%s', err);
                            end
                            findb = false;
                            for ii = linspace(max(left, 0), right)
                                ccBoundMax = min((beta - ii * gammaa) ./ tao);
                                ccBoundMin = max((alpha - ii * gammaa) ./ tao);
                                if ccBoundMax >= ccBoundMin  %...
                                    %                         && (dontCareWingShape  ...
                                    %                         || ((abs(originb) < 5e-4                        && (abs(ii) < 10 * abs(originb))) ...
                                    %                         || (abs(originb) > 5e-4 && abs(originb) < 1e-3 && (abs(ii) < 2.5 * abs(originb) && abs(ii) > abs(originb)/2.5)) ...
                                    %                         || (abs(originb) > 1e-3                        && (abs(ii) < 1.5 * abs(originb) && abs(ii) > abs(originb)/1.5))))
                                    bb = ii;
                                    findb = true;
                                    
                                    if  ((abs(originb) < 5e-4                        && (abs(bb) > 10 * abs(originb))) ...
                                      || (abs(originb) > 5e-4 && abs(originb) < 1e-3 && (abs(bb) > 2.5 * abs(originb) || abs(bb) < abs(originb)/2.5)) ...
                                      || (abs(originb) > 1e-3                        && (abs(bb) > 1.5 * abs(originb) || abs(bb) < abs(originb)/1.5)))
                                        if ~flat && bb ~= 0
                                            hugeChange = true;
                                        end
                                    end
                                    break;
                                end
                            end
                            if (~findb)
                                err = 'Right fail to extrapolate with given boundary';
                                error('%s', err);
                            end
                            %                     if (~dontCareWingShape && (abs(bb) > 3 * abs(originb) || abs(bb) < 0.3 * abs(originb)))
                            %                         if bb ~= 0
                            %                             err = 'Extrapolate right will change slope a lot';
                            %                             error('%s', err);
                            %                         end
                            %                     end
                        end
                        maxCC = min(maxCC, ccBoundMax);
                        minCC = max(minCC, ccBoundMin);
                    end
                    if concave && cc > maxCC
                        cc = minCC;
                    else
                        cc = min(max(cc, minCC), maxCC);
                        if ~isnan(tailConcavity)
                            cc = minCC + tailConcavity * (cc - minCC);
                        end
                    end
                    
                    dd = (-bb - 2 * cc * hright) / 3 / h2;
                    
                    success = true;
                catch error1
                    if (triedextended)
                        throw(error1);
                    else
                        lb = lbmin;
                        ub = ubmax;
                        triedextended = true;
                    end
                end
            end
        end
        
        function [cc] = parsToFitBoundaryc(t, aa, bb, cc, dd, xEnd, aMin, aMax)
            if ~isempty(xEnd)
                delta = xEnd - t;
                delta2 = delta .^ 2;
                delta3 = delta .^ 3;
                
                left = max((aMin - aa - bb * delta - dd * delta3) ./ delta2);
                right = min((aMax - aa - bb * delta - dd * delta3) ./ delta2);
                
                if (left > right)
                    err = 'Right fail to extrapolate with given boundary';
                    error('%s', err);
                end
            end
            
            %            tmp = 1 - x^2;
            %            ccadj = cc * tmp;
            %            ccadj = min(max(ccadj,left),right);
            %            cc = ccadj / tmp;
            cc = min(max(cc, left), right);
            %
            %            % make sure after c change's a still valid
            %            aa = a - bb * x - cc * x^2 - dd * x^3;
            %            left = max((aMin - aa - bb * delta - dd * delta3) ./ delta2);
            %            right = min((aMax - aa - bb * delta - dd * delta3) ./ delta2);
            %
            %            if (left > right)
            %                err = 'Fail to interpolate with given boundary';
            %                error('error in quadprog: %s', err);
            %            end
        end
        
        function [bb] = parsToFitBoundaryb(t, aa, bb, cc, dd, xEnd, aMin, aMax)
            if ~isempty(xEnd)
                delta = xEnd - t;
                delta2 = delta .^ 2;
                delta3 = delta .^ 3;
                
                %                 left = max((aMin - aa - bb * delta - dd * delta3) ./ delta2);
                %                 right = min((aMax - aa - bb * delta - dd * delta3) ./ delta2);
                if (min(delta)) > 0
                    left = max((aMin - aa - dd * delta3 - cc * delta2) ./ delta);
                    right = min((aMax - aa - dd * delta3 - cc * delta2) ./ delta);
                elseif (max(delta)) < 0
                    left = max((aMax - aa - dd * delta3 - cc * delta2) ./ delta);
                    right = min((aMin - aa - dd * delta3 - cc * delta2) ./ delta);
                else
                    err = 'Right fail to extrapolate with given boundary';
                    error('%s', err);
                end
                
                if (left > right)
                    err = 'Right fail to extrapolate with given boundary';
                    error('%s', err);
                end
            end
            
            %bbold = bb;
            bb = min(max(bb, left), right);
        end
        
        %         function [bb, cc, dd] = parsToFitBoundary(t, aa, bb, cc, dd, xEnd, aMin, aMax)
        %             if ~isempty(xEnd)
        %                 delta = xEnd - t;
        %                 delta2 = delta .^ 2;
        %                 delta3 = delta .^ 3;
        %
        % %                 left = max((aMin - aa - bb * delta - dd * delta3) ./ delta2);
        % %                 right = min((aMax - aa - bb * delta - dd * delta3) ./ delta2);
        %                 if (min(delta)) > 0
        %                     left = max((aMin - aa - dd * delta3 - cc * delta2) ./ delta);
        %                     right = min((aMax - aa - dd * delta3 - cc * delta2) ./ delta);
        %                 elseif (max(delta)) < 0
        %                     left = max((aMax - aa - dd * delta3 - cc * delta2) ./ delta);
        %                     right = min((aMin - aa - dd * delta3 - cc * delta2) ./ delta);
        %                 else
        %                     err = 'Fail to interpolate with given boundary';
        %                     error('error in quadprog: %s', err);
        %                 end
        %
        %                 if (left > right)
        %                     err = 'Fail to interpolate with given boundary';
        %                     error('error in quadprog: %s', err);
        %                 end
        %             end
        %
        %             bb = min(max(bb, left), right);
        %         end
        
        function [concavePoints,turningPoint, potentials] = calConcavePoints(x, n, originSecD, turningPoint, leftRight, flat, usemax)
            sumd = sum(originSecD);
            potentials = [];
            if (leftRight == -1 && ~flat) || (leftRight == 1 && flat) % flat on right wing should be concave then convex, just like a left wing
                if isnan(turningPoint)
                    if usemax
                        if (sumd < -0.05 && max(originSecD) < 0.01) % if even most convex point doesn't make the curve convex, all poinst should be concave
                            concavePoints = n-2;
                            turningPoint = x(end-1);
                            return;
                        elseif (sumd > 0.05 && min(originSecD) > -0.01) 
                            concavePoints = 0;
                            turningPoint = x(1);
                            return;
                        end
                        if (sumd > 1e-3)
                            potentials = [0 x(1)];
                        elseif (sumd < -1e-3)
                            potentials = [n-2 x(end-1)];
                        elseif (max(originSecD) < 0.01 && min(originSecD) > -0.01)
                            potentials = [0 x(1); n-2 x(end-1)];
                        end
                        %find the maximum convexity point, then go to the next point it turns concave
                        revSecD = fliplr(originSecD);
                        ss = cumsum(revSecD);
                        convexPeak = find(ss == max(ss));
                        if length(convexPeak) > 1
                            if convexPeak(end) == length(ss)
                                convexPeak = convexPeak(end-1);
                            else
                                convexPeak = convexPeak(end);
                            end
                        end
                        % test if just only one huge outlier concave
                        if convexPeak == length(ss)
                            concavePoints = 0;
                            turningPoint = x(1);
                        else
                            turningPointIdx = n - (convexPeak + 2) + 1;
                            turningPoint = x(turningPointIdx);
                            concavePoints = turningPointIdx - 1; %minus 1 b/c the first one is not auto 0 concave
                        end
                    else
                        %find the point where 2/3 of previous point are concave
                        idx = find(cumsum(originSecD <= 0) >= ((1:1:n-2) * 2/3));
                        if isempty(idx)
                            concavePoints = 0;
                            turningPoint = x(1);
                        else
                            idxx = find(originSecD(1:idx(end)) <= 0);
                            concavePoints = idxx(end);
                            turningPoint = x(concavePoints + 1);
                        end
                    end
                else
                    if turningPoint < x(2)
                        concavePoints = 0;
                    elseif turningPoint >= x(end-1)
                        concavePoints = n-2;
                    else
                        idx = find(x(2:end-1)<= turningPoint);
                        if isempty(idx)
                            concavePoints = 0;
                        else
                            concavePoints = idx(end);
                        end
                    end
                end
            else
                if isnan(turningPoint)
                    if usemax
                        if (sumd < -0.05 && max(originSecD) < 0.01) % if even most convex point doesn't make the curve convex, all poinst should be concave
                            concavePoints = n-2;
                            turningPoint = x(2);
                            return;
                        elseif (sumd > 0.05 && min(originSecD) > -0.01)
                            concavePoints = 0;
                            turningPoint = x(end);
                            return;
                        end
                        if (sumd > 1e-3)
                            potentials = [0 x(end)];
                        elseif (sumd < -1e-3)
                            potentials = [n-2 x(2)];
                        elseif (max(originSecD) < 0.01 && min(originSecD) > -0.01)
                            potentials = [0 x(end); n-2 x(2)];
                        end
                        %find the maximum convexity point, then go to the next point it turns concave
                        ss = cumsum(originSecD);
                        convexPeak = find(ss == max(ss));
                        if length(convexPeak) > 1
                            if convexPeak(end) == length(ss)
                                convexPeak = convexPeak(end-1);
                            else
                                convexPeak = convexPeak(end);
                            end
                        end
                        % test if just only one huge outlier concave
                        if convexPeak == length(ss)
                            concavePoints = 0;
                            turningPoint = x(end);
                        else
                            turningPointIdx = convexPeak + 2;
                            turningPoint = x(turningPointIdx);
                            concavePoints = n - turningPointIdx + 1 - 1; %minus one b/c last one auto 0 concave
                        end
                    else
                        idx = find(cumsum(fliplr(originSecD) <= 0) >= ((1:1:n-2) * 2/3));
                        idx = n-1 - idx ;
                        if isempty(idx)
                            concavePoints = 0;
                            turningPoint = x(end);
                        else
                            idxx = find(originSecD(idx(end):end) <=0);
                            turningPointIdx = idxx(1) + idx(end) - 1;
                            concavePoints = n - 1 - turningPointIdx;
                            turningPoint = x(turningPointIdx + 1);
                        end
                    end
                else
                    if turningPoint <= x(2)
                        concavePoints = n-2;
                    elseif turningPoint > x(end-1)
                        concavePoints = 0;
                    else
                        idx = find(x(2:end-1)>turningPoint);
                        if isempty(idx)
                            concavePoints = 0;
                        else
                            turningPointIdx = idx(1);
                            concavePoints = n - 1 - turningPointIdx;
                        end
                    end
                end
            end
        end
        
        function [gcv] = calculateGCV(y, g, w, smoothCoeff, Q, R)
            n = length(R);
            traceA = sum(calculateAii(Q, R, w, smoothCoeff));
            gcv = w * ((y'-g).^2) / ((1 - traceA/n)^2) / n;
            %gcv = sum((y'-g).^2) / ((1 - traceA/n)^2) / n;
        end
        
        function [cv] = calculateCV(y, g, w,smoothCoeff, Q, R)
            n = length(R);
            Aii = calculateAii(Q,R,w,smoothCoeff);
            cv = w * ((y'-g)./(1-Aii)').^2 / n;
            %cv = sum(((y'-g)./(1-Aii)').^2) / n;
        end
        
        % does moving average on signal x, window size is w
        function y = movingAverage(x, w)
            k = ones(1, w) / w;
            y = conv(x, k, 'same');
        end
    end
end

function [Aii] = calculateAii(Q, R, w, smoothCoeff)
W = diag(w);
B = R + smoothCoeff * (Q' /W * Q);
%B = R + smoothCoeff * (Q' * Q);
[l, D] = ldl(B);
n = length(B);
b = zeros(n,n);
d = diag(D);
% calculate inverse b
if (length(B) < 4)
    b = inv(B);
else
    b(n,n) = 1/d(n);
    b(n-1,n) = -l(n,n-1) * b(n,n);
    b(n-2,n) = -l(n-1,n-2) * b(n-1,n) - l(n,n-2) * b(n,n);
    b(n,n-1) = b(n-1, n);
    b(n,n-2) = b(n-2, n);
    
    b(n-1,n-1) = 1/d(n-1) - l(n,n-1) * b(n-1,n);
    b(n-2,n-1) = -l(n-1,n-2) * b(n-1,n-1) - l(n,n-2) * b(n-1,n);
    b(n-3,n-1) = -l(n-2,n-3) * b(n-2,n-1) - l(n-1, n-3) * b(n-1,n-1);
    b(n-1,n-2) = b(n-2,n-1);
    b(n-1,n-3) = b(n-3,n-1);
    
    
    for i = n-2 : -1 : 3
        b(i,i) = 1/d(i) - l(i+1, i)*b(i,i+1) - l(i+2,i)*b(i,i+2);
        b(i-1,i) = -l(i,i-1) * b(i,i) - l(i+1,i-1) * b(i, i+1);
        b(i-2,i) = -l(i-1, i-2) * b(i-1,i) - l(i, i-2) * b(i,i);
        b(i,i-1) = b(i-1,i);
        b(i,i-2) = b(i-2,i);
    end
    
    b(2,2) =1/d(2) - l(3,2)*b(2,3) - l(4,2)*b(2,4);
    b(1,2) = -l(2,1) * b(2,2) - l(3,1) * b(2,3);
    b(2,1) = b(1,2);
    
    b(1,1) = 1/d(1) - l(2,1) * b(1,2) - l(3,1) * b(1,3);
end

%tmp is w-1*Q/B(=*b)*Q'
if (length(B) < 4)
    tmp = diag(Q*b*Q')'./w;
    %tmp = diag(Q*b*Q')';
else
    tmp = zeros(1,n+2);
    %tmp(1) = Q(1, 1) ^ 2 * b(1,1) + Q(1,2) ^ 2 * b(2,2) + 2 * Q(1,1) * Q(1,2) * b(1,2);
    tmp(1) = Q(1,1) * (Q(1, 1) * b(1, 1)) / w(1);
    tmp(2) = (Q(2,1) * (Q(2, 1) * b(1, 1) + Q(2, 2) * b(1, 2)) ...
        + Q(2,2) * (Q(2, 1) * b(2, 1) + Q(2, 2) * b(2, 2))) / w(2);
    for i = 3:n
        %tmp(i) = Q(i,i-1) ^2 * b(i-1,i-1) + Q(i,i) ^ 2 * b(i,i) + Q(i,i+1) ^ 2 * b(i+1, i+1) + 2 * Q(i, i-1) * Q(i,i) * b(i-1,1) + 2 * Q(i,i-1) * Q(i, i+1) * b(i-1, i+1) + 2 * Q(i,i) * Q(i, i+1) * b(i, i+1);
        tmp(i) = (Q(i,i-2) * (Q(i, i-2) * b(i-2, i-2) + Q(i, i-1) * b(i-2, i-1) + Q(i, i) * b(i-2, i)) ...
            + Q(i,i-1) * (Q(i, i-2) * b(i-1, i-2) + Q(i, i-1) * b(i-1, i-1) + Q(i, i) * b(i-1, i)) ...
            + Q(i,i  ) * (Q(i, i-2) * b(i  , i-2) + Q(i, i-1) * b(i  , i-1) + Q(i, i) * b(i, i))) / w(i);
    end
    tmp(n+1) = (Q(n+1,n-1) * (Q(n+1, n-1) * b(n-1, n-1) + Q(n+1, n) * b(n-1, n)) ...
        + Q(n+1,n )  * (Q(n+1, n-1) * b(n  , n-1) + Q(n+1, n) * b(n  , n))) / w(n+1);
    %tmp(n) = Q(n, n-1) ^ 2 * b(n-1, n-1) + Q(n,n) ^ 2 * b(n, n) + 2 * Q(n, n-1) * Q(n,n) * b(n-1, n);
    tmp(n+2) = Q(n+2,n  ) * (Q(n+2, n  ) * b(n, n)) / w(n+2);
end
%traceA = size(Q,1) - smoothCoeff * sum(tmp);
Aii = 1 - smoothCoeff * tmp;
end

function [list, k] = firstn(origin, n)
k = min(n, length(origin));
list = origin(1: k);
end

function [list] = lastn(origin, n)
k = min(n, length(origin));
list = origin(length(orgin) - k + 1: end);
end
