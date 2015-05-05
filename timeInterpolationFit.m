function [smoothCoeff1, exitflag, g, gamma] = timeInterpolationFit(x, y, weight, smoothCoeff, fixIdx, leftRight, upperLimitG, lowerLimitG, invalidx, invalidupper, invalidlower)
% clc
% M = csvread('c:\temp\voltooltest\slice_FXY.USZ_20150116_right.csv', 1);
% leftRight = 1;
% fixIdx = [];
% smoothCoeff = 1e-7;
% ivalid = find(~isnan(M(:,8)));
% iinvalid = find(isnan(M(:,8)));
% x = M(ivalid,2)';
% invalidx = M(iinvalid,2)';
% y = M(ivalid,8)';
% weight = M(ivalid,7)';
% h = x(2:end) - x(1:end-1);
% n = length(x);
% upperLimitG = M(ivalid,9)';%inf(1,n);
% upperLimitG(upperLimitG == 1) = inf;
% invalidupper = M(iinvalid, 9)';
% invalidlower = M(iinvalid, 10);
% lowerLimitG = M(ivalid,10)';%zeros(1,n);

h = x(2:end) - x(1:end-1);
n = length(x);

Q = zeros(n, n-2);

for i = 1 : n
    for j = 1: n-2
        if i == j
            Q(i,j) = 1/h(i);
        elseif i-1 == j
            Q(i,j) = - 1/h(i-1) - 1/h(i);
        elseif i-2 == j
            Q(i,j) = 1/h(i-1);
        end
    end
end

R = zeros(n-2,n-2);

for i = 1:n-2
    for j = 1:n-2
        if i-1 == j
            R(i,j) = h(j+1)/6;
        elseif i == j
            R(i,j) = (h(i) + h(i+1))/3;
        elseif i+1 == j
            R(i,j) = h(i+1)/6;
        end
    end
end

A = [Q; -R'];
% A'g = 0, also the fix values
Aeq = A';
Beq = zeros(n-2, 1);
if (~isempty(fixIdx))
    for i = 1 : length(fixIdx)
        idx = fixIdx(i);
        tmpBoundx = [zeros(1, idx), 1, zeros(1, n-1-idx), zeros(1,n-2)];
        Aeq = [Aeq; tmpBoundx];
        Beq = [Beq; y(idx+1)];
    end
end

originFirstD = (y(3:n) - y(1:n-2)) ./ (h(1:end-1) + h(2:end));
originSecD = ((y(1:n-2) - y(2:n-1))./h(1:end-1) - (y(2:n-1) - y(3:n))./h(2:end))./((h(1:end-1) + h(2:end))/2);
if leftRight == -1 && mean(originFirstD(1:min(3, n-2))) > 0 && y(1) == min(y)
    % if left wing is monotone increasing which is abnormal, we make left
    % wing monotone increasing and extrapolate a flat wing
    [concavePoints, ~] = calConcavePoints(x, n, originSecD, NaN, leftRight, false, true);
    [concavePoints2, ~] = calConcavePoints(x, n, originSecD, NaN, leftRight, false, false);
    %b1 >= 0 (-b1 <= 0)
    Ales = [1/h(1), -1/h(1), zeros(1, n-2), h(1)/6, zeros(1, n-3)];
    bles = 0;
elseif leftRight == 1 && mean(originFirstD(n-2-min(3,n-2)+1:end)) < 0 && y(end) == min(y)
    % if right wing is monotone decreasing which is abnormal, we make right
    % wing monotone decreasing and extrapolate a flat wing
    [concavePoints, ~] = calConcavePoints(x, n, originSecD, NaN, leftRight, false, true);
    [concavePoints2, ~] = calConcavePoints(x, n, originSecD, NaN, leftRight, false, false);
    %bn <= 0
    Ales = [zeros(1,n-2), -1/h(n-1), +1/h(n-1), zeros(1, n-3), +h(n-1)/6];
    bles = 0;
elseif (isnan(leftRight))
    concavePoints = 0;
    concavePoints2 = 0;
    Ales = [];
    bles = [];
else
    [concavePoints, ~] = calConcavePoints(x, n, originSecD, NaN, leftRight, false, true);
    [concavePoints2, ~] = calConcavePoints(x, n, originSecD, NaN, leftRight, false, false);
    % Ales/bles, make sure strictly monotone at the end by setting leftwing b1 < -1e-7 and bn > 1e-7 (-bn < -1e-7)
    if leftRight == -1
        Ales = [-1/h(1), +1/h(1), zeros(1, n-2), -h(1)/6, zeros(1, n-3)];%; zeros(1,n-2), 1/h(n-1), -1/h(n-1), zeros(1, n-3), -h(n-1)/6];
    else
        Ales = [zeros(1,n-2), +1/h(n-1), -1/h(n-1), zeros(1, n-3), -h(n-1)/6];
    end
    bles = -1e-7;
end

% add the inside boundary if exists (when there are invalid data points
% between valid points, we dont have a goal but we do have boundary)
if ~isempty(invalidx)
    for i = 1:length(invalidx)
        tt = invalidx(i);
        tupper = invalidupper(i);
        tlower = invalidlower(i);
        
        if (tt <= x(1) || tt >= x(end)) % extrapolating part
            % do nothing for now, will fix later
        else
            tp1idx = find(x>tt,1);
            tiidx = tp1idx - 1;
            tp1 = x(tp1idx);
            ti = x(tiidx);
            %         catch e
            %             % we should not be here b/c we have smooth region data
            %             baseException = MException('flexWingFit:badData', 'tt should not be the first or last');
            %             ee = addCause(baseException, e);
            %             throw(ee);
            
            hh = tp1 - ti;
            if (tp1 == x(end)) % if left boundary
                Ales = [Ales; zeros(1, tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1, n-tp1idx), zeros(1,tiidx-2), -(tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6];
                Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), zeros(1,tiidx-2), (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6];
                if ((leftRight == -1 && isnan(boundarySecondDx)) || leftRight == 1)
                    bles = [bles; tupper];
                    bles = [bles; -tlower];
                else % if nonzero boundary second dx
                    bles = [bles; tupper + (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6 * boundarySecondDx];
                    bles = [bles; -tlower - (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6 * boundarySecondDx];
                end
            elseif (ti == x(1)) % if right boundary
                Ales = [Ales; zeros(1,tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1,n-tp1idx), -(tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                if ((leftRight == 1 && isnan(boundarySecondDx)) || leftRight == -1)
                    bles = [bles; tupper];
                    bles = [bles; -tlower];
                else % if nonzero boundary second dx
                    bles = [bles; tupper + (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6 * boundarySecondDx];
                    bles = [bles; -tlower - (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6 * boundarySecondDx];
                end
            else % normal
                %upper
                Ales = [Ales; zeros(1,tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1,n-tp1idx), zeros(1,tiidx-2), -(tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6, -(tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                bles = [bles; tupper];
                %lower
                Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), zeros(1,tiidx-2), (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6, (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                bles = [bles; -tlower];
            end
        end
    end
end

% lb/ub
if leftRight == -1
    lb = [zeros(n, 1); -inf(concavePoints, 1); zeros(n - 2 - concavePoints, 1)];
    ub = [inf(n, 1); zeros(concavePoints, 1); inf(n-2 - concavePoints, 1)];
    lb2 = [zeros(n, 1); -inf(concavePoints2, 1); zeros(n - 2 - concavePoints2, 1)];
    ub2 = [inf(n, 1); zeros(concavePoints2, 1); inf(n-2 - concavePoints2, 1)];
elseif (isnan(leftRight))
    lb = [zeros(n, 1); -inf(n-2,1)];
    ub = [inf(n, 1); inf(n-2,1)];
else
    lb = [zeros(n, 1); zeros(n-2 - concavePoints, 1); -inf(concavePoints, 1)];
    ub = [inf(n, 1); inf(n-2-concavePoints,1); zeros(concavePoints,1)];
    lb2 = [zeros(n, 1); zeros(n-2 - concavePoints2, 1); -inf(concavePoints2, 1)];
    ub2 = [inf(n, 1); inf(n-2-concavePoints2,1); zeros(concavePoints2,1)];
end

if ~isempty(lowerLimitG)
    lb(1:n) = lowerLimitG';
    lb2(1:n) = lowerLimitG';
end
if ~isempty(upperLimitG)
    ub(1:n) = upperLimitG';
    ub2(1:n) = upperLimitG';
end

method = 1;
smoothNan = false;
if isnan(smoothCoeff)
    smoothNan = true;
    %     smoothCoeff = 6;
    %opt = optimset('Display', 'iter','TolX', 0.6); %'Display', 'iter',
    %[smoothCoeff, gcvScore, exitflag] = fminbndWithStartPoint(@gcv, 1e-7, 0, 6, opt);% fmincon(@gcv, 1e-7, [], [], [], [], 0, 6, [],opt);
    [smoothCoeff, gcvScore, re, g, exitflag] = naiveSearch(1e-7, 0, 2);
    %     if exitflag <= 0
    %         if exitflag == 0
    %             err = 'Maximum number of function evaluations or iterations was reached';
    %         elseif exitflag == -1
    %             err = 'Algorithm was terminated by the output function';
    %         elseif exitflag == -2
    %             err = sprintf('Bounds are inconsistent : (%g > %g)', 0, 6);
    %         else
    %             err = num2str(exitflag);
    %         end
    %         error('error in optimal smoothcoeff search: %s', err);
    %     end
else
    [gcvScore, re, g, exitflag] = gcv(smoothCoeff);
    
end

if concavePoints ~= concavePoints2
    method = 2;
    if exitflag == -1
        if smoothNan
            smoothCoeff = NaN;
        end
        if isnan(smoothCoeff)
            [smoothCoeff, ~, re, g, exitflag] = naiveSearch(1e-7, 0, 2);
        else
            [~, re, g, exitflag] = gcv(smoothCoeff);
        end
    else
        [gcvScore2, ree, gg, exitflagg] = gcv(smoothCoeff);
        if gcvScore2 >= gcvScore
            method = 1;
        else
            re = ree;
            g = gg;
            exitflag = exitflagg;
        end
    end
end

if exitflag < 0
    if exitflag == -2
        err = 'Problem is infeasible';
    elseif exitflag == -3
        err = 'Problem is unbounded';
    elseif exitflag == -6
        err = 'Nonconvex problem detected';
    else
        err = num2str(exitflag);
    end
    error('error in quadprog: %s', err);
end

gamma = [0; re(n+1:end); 0];
smoothCoeff1 = smoothCoeff;

%print
disp('1:n; x; y; g; upper; lower;')
[(1:1:n)' x' y' g upperLimitG' lowerLimitG']
plot(x,y,x',g);

    function [xf, fval, re, g, exitflag] = naiveSearch(f0,ax,bx)
        [f, re1, gg1, exitflag1] = gcv(f0);
        [f2, re2, gg2, exitflag2] = gcv(bx);
        [f3, re3, gg3, exitflag3] = gcv((ax + bx)/2);
        
        if f < f2
            xf = f0;
            fval = f;
            re = re1;
            g = gg1;
            exitflag = exitflag1;
        else
            xf = bx;
            fval = f2;
            re = re2;
            g = gg2;
            exitflag = exitflag2;
        end
        
        if f3 < fval
            xf = (ax + bx)/2;
            fval = f3;
            re = re3;
            g = gg3;
            exitflag = exitflag3;
        end
    end


    function [score, re, gg, exitflag] = gcv(smoothCoeff)
        try
            [re, gg, exitflag] = calculation(smoothCoeff, method);
            score = calculateGCV(y, gg, weight, smoothCoeff, Q, R);
        catch e
            re = nan;
            gg = nan;
            exitflag = -1;
            score = 1e100;
        end
    end

    function [re, g, exitflag] = calculation(smoothCoeff, method)
        D = blkdiag(diag(weight), smoothCoeff*R);
        eta = [weight.*y zeros(1, n-2)]';
        
        if smoothCoeff == 0
            [re,~,exitflag]= quadprog(D, -eta, [], [], Aeq, Beq, [], []);
        else
            if method == 1
                [re,~,exitflag]= quadprog(D, -eta, Ales, bles, Aeq, Beq, lb, ub);
            else
                [re,~,exitflag]= quadprog(D, -eta, Ales, bles, Aeq, Beq, lb2, ub2);
            end
        end
        g = re(1:n);
    end
end

function [concavePoints,turningPoint] = calConcavePoints(x, n, originSecD, turningPoint, leftRight, flat, usemax)
if (leftRight == -1 && ~flat) || (leftRight == 1 && flat) % flat on right wing should be concave then convex, just like a left wing
    if isnan(turningPoint)
        if usemax
            %find the maximum convexity point, then go to the next point it
            %turns concave
            revSecD = fliplr(originSecD);
            ss = cumsum(revSecD);
            if (max(ss) < 0) % if even most convex point doesn't make the curve convex, all poinst should be concave
                concavePoints = length(x)-2; % last point has 0 or fixed concavity
                turningPoint = x(end-1);
                return;
            end
            convexPeak = find(ss == max(ss));
            if convexPeak == length(ss)
                concavePoints = 0;
                turningPoint = x(1);
            else
                turningPointIdx = n - (convexPeak + 2) + 1;
                turningPoint = x(turningPointIdx);
                concavePoints = turningPointIdx - 1; %minus 1 b/c the first one is not auto 0 concave
            end
        else
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
            %find the maximum convexity point, then go to the next point it
            %turns concave
            ss = cumsum(originSecD);
            if (max(ss) < 0) % if even most convex point doesn't make the curve convex, all poinst should be concave
                concavePoints = length(x)-2; % last point has 0 or fixed concavity
                turningPoint = x(2);
                return;
            end
            convexPeak = find(ss == max(ss));
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
traceA = calculateTraceA(Q, R, w, smoothCoeff);
gcv = w*((y'-g).^2) / ((1 - traceA/n)^2);
end

function [traceA] = calculateTraceA(Q, R, w, smoothCoeff)
W = diag(w);
B = R + smoothCoeff * (Q' /W * Q);
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
traceA = size(Q,1) - smoothCoeff * sum(tmp);
end
