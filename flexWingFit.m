%
% letfRight: -1 means left, 1 means right
% exitflag: 3 means one side flat
%           4 means left side flat
%           5 means right side flat
%           6 means both sides flat
function [smooth1, smoothCoeff1, exitflag, g, gamma, aa, bb, cc, dd, turningPoint, xin, flatregion] = flexWingFit(xin, yin, weight, stationarypoint, tailConcavity, smoothCoeff, turningPoint, boundaryx, boundarydx, boundarydxx, leftright, xinub, xinlb, ...
    xinubmax, xinlbmin, xendl, ubendl, lbendl, ubendlmax, lbendlmin, xendr, ubendr, lbendr, ubendrmax, lbendrmin, invalidx, invalidupper, invalidlower, invaliduppermax, invalidlowermin, leftincrease, rightincrease, smooth, tight, minxrange, concave, allowflat,...
    fromtime, originalx, breakBoundary, forceclose, forcesmooth, careConvexConcave, allowHugeWingChange)
% clc
% M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
% % % MM = M(M(:,5)<=0.5, :);
% leftright = 1;
% IV = MM(MM(:,3)~=0 & MM(:,4)~=0,:);
% IV = [IV (IV(:,3)+IV(:,4))./2];
% x = IV(:,2)';
% y = IV(:,6)';
% weight = ones(1,length(x));

% clc
% M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150220_right.csv', 1);
% leftright = 1;
% smoothCoeff = nan;
% stationaryPoint = [150,nan];
% tailConcavity = [nan,nan];
% turningPoint = [nan,nan]; % 0 to n16
% boundaryx = [nan,nan];
% boundarydx = [nan,nan];
% boundarydxx = [nan,nan];
% i = find(~isnan(M(:,4)));
% if isnan(leftright)
%     intropart = i(1) : i(end);
%     iEndl = 1 : i(1) - 1;
%     iEndr = i(end) + 1 : size(M,1);
%     ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
%     iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
% elseif leftright == -1
%     intropart = i(1) : size(M,1);
%     iEnd = 1 : i(1) - 1;
%     ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
%     iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
% else
%     intropart = 1: i(end);
%     iEnd = i(end) + 1 : size(M,1);
%     ivalid = find(~isnan(M(intropart, 4)));
%     iinvalid = find(isnan(M(intropart, 4)));
% end
% x = M(ivalid,2)';
% invalidx = M(iinvalid,2)';
% y = M(ivalid,4)';
% weight = M(ivalid,5)';
% dxxleft = NaN;%3.758e-6;
% dxxright = nan;
% h = x(2:end) - x(1:end-1);
% n = length(x);
% upperLimitG = M(ivalid,7)';%inf(1,n);
% upperLimitG(upperLimitG == 1) = inf;
% invalidupper = M(iinvalid, 7)';
% invalidupper(invalidupper == 1) = inf;
% invalidlower = M(iinvalid, 6);
% lowerLimitG = M(ivalid,6)';%zeros(1,n);
% if isnan(leftright)
%     xEndl = M(iEndl, 2)';
%     xEndr = M(iEndr, 2)';
%     aMaxl = M(iEndl, 7)';
%     aMaxl(aMaxl == 1) = inf;
%     aMaxr = M(iEndr, 7)';
%     aMaxr(aMaxr == 1) = inf;
%     aMinl = M(iEndl, 6)';
%     aMinr = M(iEndr, 6)';
% elseif leftright == -1
%     xEndl = M(iEnd,2)';
%     aMaxl = M(iEnd,7)';
%     aMaxl(aMaxl == 1) = inf;
%     aMinl = M(iEnd,6)';
% else
%     xEndr = M(iEnd,2)';
%     aMaxr = M(iEnd,7)';
%     aMaxr(aMaxr == 1) = inf;
%     aMinr = M(iEnd,6)';
% end
% leftincrease = -inf;
% rightincrease = inf;

% op.x = x;
% op.y = y;
% op.w = weight;
% op.sp = stationaryPoint;
% op.tc = tailConcavity;
% op.sc = smoothCoeff;
% op.tp = turningPoint;
% op.bx = boundaryx;
% op.bdx = boundarydx;
% op.bdxx = boundarydxx;
% op.lr = leftright;
% op.ub = upperLimitG;
% op.lb = lowerLimitG;
% op.xel = xEndl;
% op.amaxl = aMaxl;
% op.aminl = aMinl;
% op.xer = xEndr;
% op.amaxr = aMaxr;
% op.aminr = aMinr;
% op.ix = invalidx;
% op.iub = invalidupper;
% op.ilb = invalidlower;
% op.li = leftincrease;
% op.ri = rightincrease;
% op.sm = smooth;
% op.t = tight;
% op.tlb = tightlb;
% op.tub = tightub;
% op.mx = minxrange;
% op.c = concave;
% op.a = allowflat;

displayOn = false;
if displayOn
    optfminbnd = optimset('Display', 'iter');
    optquad = optimoptions('quadprog','Display', 'iter');
else
    optfminbnd = optimset('Display', 'off');
    optquad = optimoptions('quadprog','Display', 'off');
end

if ~isnan(leftright)
    error('not nan leftright');
end

h = xin(2:end) - xin(1:end-1);
n = length(xin);

tight = tight - 1;
originFirstD = (yin(3:n) - yin(1:n-2)) ./ (h(1:end-1) + h(2:end));
originSecD = ((yin(3:n) - yin(2:n-1))./h(2:end) - (yin(2:n-1) - yin(1:n-2))./ h(1:end-1))./((h(1:end-1) + h(2:end))/2);
originTurningPoint = turningPoint;
originSmooth = smooth;
originSmoothCoeff = smoothCoeff;
% % if have huggge dxx, we want to loose convex/concave requirement in that
% % region if we require very close
% avg = mean(abs(originSecD));
% hugedxxregion = find(abs(originSecD) > avg * 5);

goodboundary = false;
boundarydchanged = false;
boundarychanged = false;
scchanged = false;
goodSc = false;
goodBdr = false;
countc = 0;
allowignorelbub = false;
while ~goodBdr
    if (allowignorelbub)
        invalidlower = invalidlowermin;
        invalidupper = invaliduppermax;
        xinlb = xinlbmin;
        xinub = xinubmax;
    end
    [increaseregion, decreaseregion] = findIDRegion(xinlb, xinub);
    % if length(increaseregion) == 1
    %     minincrease = x(increaseregion);
    % else
    if length(increaseregion) == 1
        minincrease = xin(increaseregion);
    else
        minincrease = [];
    end
    if length(decreaseregion) == 1
        maxdecrease = xin(decreaseregion);
    elseif ~isempty(decreaseregion)
        maxdecrease = xin(decreaseregion(end));
    else
        maxdecrease = [];
    end
    
    if ~isempty(minincrease) && ~isempty(maxdecrease) && minincrease <= maxdecrease
        if (~boundarychanged && ~allowignorelbub)
            allowignorelbub = true;
            goodSc = false;
            goodboundary = false;
            boundarychanged = true;
            goodBdr = false;
        else
            error('cannot make monotone b/c of boundary');
        end
    else
        while ~goodSc || ~goodboundary
            if smooth == 2
                smoothCoeff = 10 * exp(-50/originalx);
            end
            xleft = boundaryx(1);
            xright = boundaryx(2);
            dxleft = boundarydx(1);
            dxright = boundarydx(2);
            dxxleft = boundarydxx(1);
            dxxright = boundarydxx(2);
            
            % if ~isnan(xleft)
            %     upperLimitG(1) = inf;
            %     lowerLimitG(1) = 0;
            % end
%             if ~isnan(dxleft)
%                 inRegionDx = (y(2) - y(1)) /h(1);
%                 dxleft = (dxleft + inRegionDx)/2;
%             end
%             
%             if ~isnan(dxright)
%                 inRegionDx = (y(end) - y(end-1)) / h(end);
%                 dxright = (dxright + inRegionDx)/2;
%             end
            
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
            % A'g = 0, and set boundary x/dx if exists
            Aeq = A';
            Beq = zeros(n-2, 1);
            if smoothCoeff ~= 0
                if ~isnan(xright)
                    ABoundx = [zeros(1, n-1), 1, zeros(1,n-2)];
                    Aeq = [Aeq; ABoundx];
                    Beq = [Beq; xright];
                end
                if ~isnan(xleft)
                    ABoundx = [1, zeros(1, 2*n-3)];
                    Aeq = [Aeq; ABoundx];
                    Beq = [Beq; xleft];
                end
                if ~isnan(dxright)
                    ABounddx = [zeros(1, n-2), -1/h(n-1), +1/h(n-1), zeros(1, n-3), +h(n-1)/6];
                    if ~isnan(dxxright)
                        Beq = [Beq; dxright - h(n-1)/3 * dxxright];
                    else
                        Beq = [Beq; dxright];
                    end
                    Aeq = [Aeq; ABounddx];
                end
                if ~isnan(dxleft)
                    ABounddx = [-1/h(1), +1/h(1), zeros(1, n-2), -h(1)/6, zeros(1, n-3)];
                    if ~isnan(dxxleft)
                        Beq = [Beq; dxleft + h(1)/3 * dxxleft];
                    else
                        Beq = [Beq; dxleft];
                    end
                    Aeq = [Aeq; ABounddx];
                end
            end
            
            Ales = [];
            bles = [];
            
            %         %add the boundary b/c of extrapolating part boundaries
            %         %loose approach
            %         if ~isempty(xEndl) && ~isnan(dxxleft)
            %             if ~isnan(stationaryPoint(1))
            %                 lidx = find(aMinl > y(1));
            %                 if ~isempty(lidx)
            %                     t = x(1);
            %                     hleft = stationaryPoint(1) - t;
            %                     h2 = hleft * hleft;
            %                     for lidxi = lidx
            %                         %h3 = h2 * hleft;
            %                         deltaa = xEndl(lidxi) - t;
            %                         %deltaa2 = deltaa .^ 2;
            %                         deltaa3 = deltaa .^ 3;
            %
            %                         alpha = 3 * h2 * (aMinl(lidxi));
            %                         %beta = 3 * h2 * (aMaxl);
            %
            %                         %tao = 3 * deltaa2 * h2 - 2 * deltaa3 * hleft;
            %                         gammaaa = 3 * deltaa * h2 - deltaa3;
            %
            %                         %b - adj <= min() + dxxleft*h1/3
            %                         %tmp = gammaaa;
            %                         rightt = alpha / gammaaa + dxxleft*h(1)/3;
            %                         righttadj = 3 * h2 ./ gammaaa;
            %                         Ales = [Ales; (-1/h(1) + righttadj),  1/h(1), zeros(1,n-2), -h(1)/6, zeros(1,n-3)];
            %                         bles = [bles; rightt];
            %                         %                 %b -adj >= max() + dxleft*h1/3 => -b <= -(..)
            %                         %                 tmp = (gammaaa * hleft - tao);
            %                         %                 leftt = max(beta * hleft ./ tmp) + dxxleft*h(1)/3;
            %                         %                 lefttadj = min(-3 * h3 ./tmp);
            %                         %                 Ales = [Ales; -(-1/h(1) - lefttadj), -1/h(1), zeros(1,n-2), h(1)/6, zeros(1,n-3)];
            %                         %                 bles = [bles; -leftt];
            %                     end
            %                 end
            %             end
            %         end
            % if ~isempty(xEndr) && ~isnan(dxxright)
            %     if ~isnan(stationaryPoint(2))
            %         t = x(end);
            %         hright = stationaryPoint(2) - t;
            %         h2 = hright * hright;
            %         h3 = h2 * hright;
            %         deltaa = xEndr - t;
            %         deltaa2 = deltaa .^ 2;
            %         deltaa3 = deltaa .^ 3;
            %
            %         alpha = 3 * h2 * (aMinr);
            %         beta = 3 * h2 * (aMaxr);
            %
            %         tao = 3 * deltaa2 * h2 - 2 * deltaa3 * hright;
            %         gammaaa = 3 * deltaa * h2 - deltaa3;
            %
            %         %b - adj <= min() - dxleft*h1/3
            %         tmp = (gammaaa * hright - tao);
            %         rightt = min(beta * hright ./ tmp) - dxxright*h(n-1)/3;
            %         righttadj = max(-3 * h3./tmp);
            %         Ales = [Ales; zeros(1,n-2), -1/h(n-1), (1/h(n-1) - righttadj),  zeros(1,n-3), h(n-1)/6];
            %         bles = [bles; rightt];
            %         %b - adj >= max() - dxleft*h1/3 => -(b - adj) <= -(..)
            %         tmp = gammaaa;
            %         leftt = max(alpha ./ tmp) - dxxright*h(n-1)/3;
            %         lefttadj = min(-3 * h2./tmp);
            %         Ales = [Ales; zeros(1,n-2), 1/h(n-1) , -(-1/h(n-1) - lefttadj), zeros(1,n-3), -h(n-1)/6];
            %         bles = [bles; -leftt];
            %     end
            % end
            
            changed = false;
            minx = xin(yin == min(yin));
            if (length(minx) > 1)
                minx = minx(end);
            end
            
            [lb, lb2, ub, ub2, flat, leftflat, rightflat, potentialconvexconcaves] = calculateConcavePoints(originSecD, nan, []);
            
            method = 1;
            smoothNan =false;
            flatbff = flat;
            leftflatbff = leftflat;
            rightflatbff = rightflat;
            if isnan(smoothCoeff)
                smoothNan = true;
                %     smoothCoeff = 6;
                
                %opt = optimset('Display', 'iter','TolX', 0.6); %'Display', 'iter',
                %[smoothCoeff, gcvScore, exitflag] = fminbndWithStartPoint(@gcv, 1e-7, 0, 6, opt);% fmincon(@gcv, 1e-7, [], [], [], [], 0, 6, [],opt);
                [smoothCoeff, ~, re, g, exitflag, ~, leftflat, rightflat] = naiveSearch(flat, leftflat, rightflat, true);
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
                [~, re, g, exitflag, ~, leftflat, rightflat] = gcv(smoothCoeff, flat, leftflat, rightflat, true);
            end
            
            if exitflag < 0 && (isnan(leftright) && (concavePointsl1 ~= concavePointsl2 || concavePointsr1 ~= concavePointsr2)) || (~isnan(leftright) && (concavePoints ~= concavePoints2))
                method = 2;
%                 if exitflag == -1
                    if smoothNan
                        smoothCoeff = NaN;
                    end
                    if isnan(smoothCoeff)
                        [smoothCoeff, ~, re, g, exitflag, ~, leftflat, rightflat] = naiveSearch(flatbff, leftflatbff, rightflatbff, true);
                    else
                        [~, re, g, exitflag, ~, leftflat, rightflat] = gcv(smoothCoeff, flatbff, leftflatbff, rightflatbff, true);
                    end
%                 else
%                     %         if isnan(smoothCoeff)
%                     %             [smoothCoeff, gcvScore2, ree, gg, exitflagg] = naiveSearch(smooth);
%                     %         else
%                     %             [gcvScore2, ree, gg, exitflagg] = gcv(smoothCoeff);
%                     %         end
%                     [gcvScore2, ree, gg, exitflagg] = gcv(smoothCoeff);
%                     if exitflagg < 0 || gcvScore2 >= gcvScore
%                         method = 1;
%                     else
%                         re = ree;
%                         g = gg;
%                         exitflag = exitflagg;
%                     end
%                 end
            end;
            
            % try potential convexconcave
            fixed = true;
            if (exitflag < 0 && careConvexConcave && ~isempty(potentialconvexconcaves))
                [rows, ~] = size(potentialconvexconcaves);
                for row = 1 : 1 : rows
                    fixed = false;
                    [lb, ~, ub, ~, flat, leftflat, rightflat, ~] = calculateConcavePoints(originSecD, nan, potentialconvexconcaves(row, :));
                    method = 1;
                    if isnan(smoothCoeff)
                        [smoothCoeff, ~, re, g, exitflag, ~, leftflat, rightflat] = naiveSearch(flat, leftflat, rightflat, true);
                    else
                        [~, re, g, exitflag, ~, leftflat, rightflat] = gcv(smoothCoeff, flat, leftflat, rightflat, true);
                    end
                    if (exitflag > 0 && ~isempty(g))
                        fixed = true;
                        break;
                    end
                end
            end

            if (~fixed)
               [lb, ~, ub, ~, flat, leftflat, rightflat, ~] = calculateConcavePoints(originSecD, nan, []);
            end
            
            if isnan(dxxleft)
                dxxleft = 0;
            end
            if isnan(dxxright)
                dxxright = 0;
            end
            
            if exitflag > 0
                if isempty(g)
                    error('fail wing fit');
                end
                gamma = [dxxleft; re(n+1:end); dxxright];
                firstDx = [(g(2:n) - g(1:n-1))./h' - (2*gamma(1:n-1) + gamma(2:n))/6.*h'; (g(n) - g(n-1))/h(n-1) + h(n-1) / 6 * (gamma(n-1) + 2* gamma(n))];
                a = g(1:end); % g(line) - abcd(line,4)
                b = firstDx(1:end); % (g(line+1) - g(line))/h(line) - (gamma(line+1) + 2*gamma(line))/6*h(line) - abcd(line,5)
                c = gamma/2; % 0.5*gamma(line) - abcd(line,6)
                %d = [(gamma(2:end) - gamma(1:end-1))/ 6 ./ h'; 0]; % (gamma(line+1) - gamma(line))/6/h(line) - abcd(line,7)
            end
            
            if fromtime && ~forcesmooth && (exitflag > 0 &&((yin(1) > yin(2) && b(1) > -1e-5) || (yin(end-1) < yin(end) && b(end) < 1e-5)) && ~scchanged)
                smooth = 2;
                scchanged = true;
                goodSc = false;
            elseif exitflag == 0
                if (smooth ~= 0)
                    smooth = 0;
                else
                    smoothCoeff = 500;
                end
                scchanged = true;
                goodSc = false;
            elseif exitflag == -8 && smooth ~= 0 && ~scchanged && ~forcesmooth
                if (smooth == 2)
                    smooth = 1;
                else
                    smooth = 0;
                    smoothCoeff = nan;
                    scchanged = true;
                end
                goodSc = false;
            elseif exitflag > 0 && ~scchanged && ~forcesmooth && smooth == 0 && smoothCoeff < 70 
                window = 3;
                mask = ones(window,1)/window;
                mavg = conv(b, mask,'same');
                ratio = 0.5;
                mavgr = [(abs(mavg) <= 1e-4) .* -1e-3 + (abs(mavg) > 1e-4) .* mavg * ratio ...
                    (abs(mavg) <= 1e-4) .* 1e-3 + (abs(mavg) > 1e-4) .* mavg /ratio];
                mavgmin = min(mavgr(:,1), mavgr(:,2));
                mavgmax = max(mavgr(:,1), mavgr(:,2));
                if ~all((b<mavgmax) & (b > mavgmin))
                    smoothCoeff = 500;
                    scchanged = true;
                else
                    goodSc = true;
                end
            else
                goodSc = true;
            end
            
            if (exitflag == -2 && (~all(isnan(boundarydxx)) || ~all(isnan(boundarydx))) && ~boundarydchanged)
                boundarydx = [nan, nan];
                boundarydxx = [nan, nan];
                boundarydchanged = true;
                goodboundary = false;
            else
                goodboundary = true;
            end
            
            countc = countc + 1;
            if countc > 4
                error('Fail to fit');
            end
        end
        % if still fail, try to change boundary by allowing lbmin ubmax
        if exitflag < 0 && ~boundarychanged
            allowignorelbub = true;
            %lowerLimitG = ones(1, length(lowerLimitG)) * -inf;
            %upperLimitG = ones(1, length(lowerLimitG)) * inf;
            goodSc = false;
            goodboundary = false;
            boundarychanged = true;
            goodBdr = false;
        else
            goodBdr = true;
        end
    end
end

if isnan(leftright)
    if method == 1
        turningPoint(1) = tpl1;
        turningPoint(2) = tpr1;
    else
        turningPoint(1) = tpl2;
        turningPoint(2) = tpr2;
    end
else
    if method == 1
        turningPoint(1) = tp;
    else
        turningPoint(1) = tp2;
    end
end

if exitflag <= 0
    if exitflag == 0
        err = 'Fail to converge';
    elseif exitflag == -2
        err = 'Problem is infeasible';
    elseif exitflag == -3
        err = 'Problem is unbounded';
    elseif exitflag == -6
        err = 'Nonconvex problem detected';
        %     elseif exitflag == -8
        %         err = 'Fail to make sure monotone after 4 iterations';
    else
        err = num2str(exitflag);
    end
    error('error in quadprog: %s', err);
end

% interpolation
%a. abcd(line,4) + abcd(line,5) * (t - abcd(line,2)) + abcd(line,6) * (t -abcd(line,2)) * (t - abcd(line,2)) + abcd(1,7) * (t - abcd(line,2)) * (t -abcd(line,2)) *(t - abcd(line,2))
%b. ((t - abcd(line, 2)) * abcd(line + 1, 3) + (abcd(line+1,2) - t) * abcd(line, 3))/h(line) - (t - abcd(line,2))*(abcd(line+1,2) - t)*((1 + (t - abcd(line,2))/h(line))*gamma(line+1) + (1+ (abcd(line+1,2)-t)/h(line)) * gamma(line))/6
%plot(x', y', x', g)

%update left/right flat (just in case, should not be here)
if ~allowflat(1) && (~leftflat && abs(b(1)) < 1.2e-5) %fromTime && 
    leftflat = true;
    b(1) = 0;
end
if ~allowflat(2) && (~rightflat && abs(b(end)) < 1.2e-5)
    rightflat = true;
    b(end) = 0;
end

if allowflat(1) && b(1) > 0
    leftflat = true;
end
if allowflat(2) && b(end) < 0
    rightflat = true;
end

leftneedsmoredata = false;
rightneedsmoredata = false;
if (~allowflat(1) && ((isnan(leftright) && leftflat) || (leftright == -1 && leftflat)))
    leftneedsmoredata = true;
end
if (~allowflat(2) && ((isnan(leftright) && rightflat) || (leftright == 1 && rightflat)))
    rightneedsmoredata = true;
end

% extrapolate
% if isnan(leftright)
if dxxleft == 0
    dxxleft = nan;
end
if dxxright == 0
    dxxright = nan;
end

needRefitBcHugeWingChange = 0;

miny = min(yin);
try
    if ~leftneedsmoredata
        if isnan(leftright) || leftright == -1
            [hugeChange, aal, bbl, ccl, ddl] = splineHelper.leftPars(xin, a, b, c, stationarypoint(1), leftflat, xendl, lbendl, ubendl, lbendlmin, ubendlmax, tailConcavity(1), dxleft, dxxleft, concave);%, dontCareWingShape);
            if (hugeChange || (yin(1) == miny && ~allowflat(1)))
                if (~allowHugeWingChange(1))
                    leftneedsmoredata = true;
                else
                    needRefitBcHugeWingChange = 1;
                end
            end
        else
            aal = 0; bbl = 0; ccl = 0; ddl = 0;
        end
    end
catch e
    if strcmp(e.message, 'Left needs more data') || strcmp(e.message, 'Extrapolate left will change slope a lot') || strcmp(e.message, 'Left fail to extrapolate with given boundary')
        leftneedsmoredata = true;
    else
        rethrow(e);
    end
end

try
    if ~rightneedsmoredata
        if isnan(leftright)
            [hugeChange, aar, bbr, ccr, ddr] = splineHelper.rightPars(xin, a, b, c, stationarypoint(2), rightflat, xendr, lbendr, ubendr, lbendrmin, ubendrmax, tailConcavity(2), dxright, dxxright, concave);%, dontCareWingShape);
            if (hugeChange || (yin(end) == miny && ~allowflat(2)))
                if (~allowHugeWingChange(2))
                    rightneedsmoredata = true;
                else
                    if (needRefitBcHugeWingChange == 1)
                        needRefitBcHugeWingChange = 3;
                    else
                        needRefitBcHugeWingChange = 2;
                    end
                end
            end
        elseif leftright == 1
            [aar, bbr, ccr, ddr] = splineHelper.rightPars(xin, a, b, c, stationarypoint(1), rightflat, xendr, lbendr, ubendr, lbendrmin, ubendrmax, tailConcavity(1), dxright, dxxright, concave);%, dontCareWingShape);
        else
            aar = 0; bbr = 0; ccr = 0; ddr = 0;
        end
    end
catch e
    if strcmp(e.message, 'Right needs more data') || strcmp(e.message, 'Extrapolate right will change slope a lot') || strcmp(e.message, 'Right fail to extrapolate with given boundary')
        rightneedsmoredata = true;
    else
        rethrow(e);
    end
end

% refit to force smooth for wing b/c huge change
if needRefitBcHugeWingChange > 0
    % left wing need to be smooth out
    if needRefitBcHugeWingChange == 1 || needRefitBcHugeWingChange == 3
        tl = xin(1);
        xexl = sort(xendl); %stationaryPoint(1) : 1 : tl-1;
        yexl = aal + bbl .* (xexl-tl) + ccl .* (xexl-tl).^2 + ddl .* (xexl-tl).^3;
%         xl = find(yexl > yin(2) * 1.1);
%         if isempty(xl)
            xl = 1;
%         end
        xl = xl(end);
        xll = xl : length(xexl);
        idx = find(xendl == xexl(xl));
        xin = [xexl(xll) xin];
        yin = [yexl(xll) yin];
        weight = [ones(1, length(xll)) * min(weight) weight];
        boundaryx = [nan nan];
        boundarydx = [nan nan];
        boundarydxx = [nan nan];
        xvalididx = arrayfun(@(x)find(xendl==x,1),xexl(xll));
        xinlb = [lbendl(xvalididx) xinlb];
        xinlbmin = [lbendlmin(xvalididx) xinlbmin];
        xinub = [ubendl(xvalididx) xinub];
        xinubmax = [ubendlmax(xvalididx) xinubmax];
        xinvalididx = find(xendl >= xin(1));
        xinvalididx = xinvalididx(~ismember(xinvalididx, xvalididx));
        invalidx = [invalidx xendl(xinvalididx)];
        invalidupper = [invalidupper ubendl(xinvalididx)];
        invaliduppermax = [invaliduppermax ubendlmax(xinvalididx)];
        invalidlower = [invalidlower lbendl(xinvalididx)];
        invalidlowermin = [invalidlowermin lbendlmin(xinvalididx)];
        xendlidx = find(xendl < xin(1));
        xendl = xendl(xendlidx);
        ubendl = ubendl(xendlidx);
        lbendl = lbendl(xendlidx);
        ubendlmax = ubendlmax(xendlidx);
        lbendlmin = lbendlmin(xendlidx);
    end
    % right wing need to be smooth out
    if needRefitBcHugeWingChange == 2 || needRefitBcHugeWingChange == 3
        tr = xin(end);
        xexr = sort(xendr);
        yexr = aar + bbr .* (xexr-tr) + ccr .* (xexr-tr).^2 + ddr .* (xexr-tr).^3;
%         xr = find(yexr > yin(end-1) * 1.1);
%         if isempty(xr)
            xr = length(xexr);
%         end
        xr = xr(1);
        xrr = 1 : xr;
        idx = xr;
        xin = [xin xexr(xrr)];
        yin = [yin yexr(xrr)];
        weight = [weight ones(1, xr) * min(weight)];
        boundaryx = [nan nan];
        boundarydx = [nan nan];
        boundarydxx = [nan nan];
        xvalididx = arrayfun(@(x)find(xendr==x,1),xexr(xrr));
        xinlb = [xinlb lbendr(xvalididx)];
        xinlbmin = [xinlbmin lbendrmin(xvalididx)];
        xinub = [xinub ubendr(xvalididx)];
        xinubmax = [xinubmax ubendrmax(xvalididx)];
        xinvalididx = find(xendr <= xin(end));
        xinvalididx = xinvalididx(~ismember(xinvalididx, xvalididx));
        invalidx = [invalidx xendr(xinvalididx)];
        invalidupper = [invalidupper ubendr(xinvalididx)];
        invaliduppermax = [invaliduppermax ubendrmax(xinvalididx)];
        invalidlower = [invalidlower lbendr(xinvalididx)];
        invalidlowermin = [invalidlowermin lbendrmin(xinvalididx)];
        xendridx = find(xendr > xin(end));
        xendr = xendr(xendridx);
        ubendr = ubendr(xendridx);
        lbendr = lbendr(xendridx);
        ubendrmax = ubendrmax(xendridx);
        lbendrmin = lbendrmin(xendridx);
    end
    
    [smooth1, smoothCoeff1, exitflag, g, gamma, aa, bb, cc, dd, turningPoint, xin, flatregion] = flexWingFit(xin, yin, weight, stationarypoint, tailConcavity, originSmoothCoeff, originTurningPoint, boundaryx, boundarydx, boundarydxx, leftright, xinub, xinlb, ...
        xinubmax, xinlbmin, xendl, ubendl, lbendl, ubendlmax, lbendlmin, xendr, ubendr, lbendr, ubendrmax, lbendrmin, invalidx, invalidupper, invalidlower, invaliduppermax, invalidlowermin, leftincrease, rightincrease, originSmooth, tight, minxrange, concave, allowflat,...
        fromtime, originalx, breakBoundary, forceclose, forcesmooth, careConvexConcave, false);
    return;
end

if leftneedsmoredata && rightneedsmoredata
    error('LR need more data');
elseif leftneedsmoredata
    error('Left needs more data');
elseif rightneedsmoredata
    error('Right needs more data');
end

aa = [aal, aar];
bb = [bbl, bbr];
cc = [ccl, ccr];
dd = [ddl, ddr];

smoothCoeff1 = smoothCoeff;
smooth1 = smooth;

flatregion = [nan, nan];
% if ~isnan(stationaryPoint(1))
if isnan(leftright)
    if ~isempty(xendl) && bb(1) == 0 && cc(1) == 0 && dd(1) == 0
        flatregion(1) = xin(1);
    elseif ~isempty(xendr) && bb(2) == 0 && cc(2) == 0 && dd(2) == 0
        flatregion(2) = xin(end);
    end
elseif leftright == -1
    if ~isempty(xendl) && bb(1) == 0 && cc(1) == 0 && dd(1) == 0
        flatregion(1) = xin(1);
    end
elseif leftright == 1
    if ~isempty(xendr) && bb(2) == 0 && cc(2) == 0 && dd(2) == 0
        flatregion(2) = xin(end);
    end
end
% end

%print
if (displayOn)
    ub(ub == 8.9) = inf;
    disp('1:n; x; y; g; upper; lower; dx; dxx')
    [(1:1:n)' xin' yin' g ub(1:n) lb(1:n) firstDx gamma]
    if isnan(leftright)
        disp('x; g; dx; dxx;')
        tl = xin(1);
        tr = xin(end);
        if ~isempty(xendl)
            xxl = sort(xendl); %stationaryPoint(1) : 1 : tl-1;
            yyl = aal + bbl .* (xxl-tl) + ccl .* (xxl-tl).^2 + ddl .* (xxl-tl).^3;
        else
            xxl = [];
            yyl = [];
        end
        if ~isempty(xendr)
            xxr = sort(xendr); %tr + 1 : 1 : stationaryPoint(2);
            yyr = aar + bbr .* (xxr-tr) + ccr .* (xxr-tr).^2 + ddr .* (xxr-tr).^3;
        else
            xxr = [];
            yyr = [];
        end
        xx = [xxl xin xxr];
        yy = [yyl g' yyr];
        
        h = xx(2:end) - xx(1:end-1);
        dx = (yy(2:end) - yy(1:end-1))./h;
        dxx = ((yy(1:end-2) - yy(2:end-1))./h(1:end-1) - (yy(2:end-1) - yy(3:end))./h(2:end))./((h(1:end-1) + h(2:end))/2);
        [xx' yy' [dxleft; dx(2:end)'; dxright] [dxxleft; dxx'; dxxright]]
        plot(xx', yy', xin', yin', xin', lb(1:n), xin', ub(1:n))
    elseif leftright == -1
        t = xin(1);
        xx = sort(xendl);
        disp('x; g; dx; dxx;')
        yy = aal + bbl .* (xx-t) + ccl .* (xx-t).^2 + ddl .* (xx-t).^3;
        xx = [xx xin];
        yy = [yy g'];
        
        h = xx(2:end) - xx(1:end-1);
        dx = (yy(2:end) - yy(1:end-1))./h;
        dxx = ((yy(1:end-2) - yy(2:end-1))./h(1:end-1) - (yy(2:end-1) - yy(3:end))./h(2:end))./((h(1:end-1) + h(2:end))/2);
        [xx' yy' [dxleft; dx(2:end)'; dxright] [dxxleft; dxx'; dxxright]]
        plot(xx', yy', xin', yin', xin', lb(1:n), xin', ub(1:n))
    else
        t = xin(end);
        xx = sort(xendr);
        disp('x; g; dx; dxx;')
        yy = aar + bbr .* (xx-t) + ccr .* (xx-t).^2 + ddr .* (xx-t).^3;
        xx = [xin xx];
        yy = [g' yy];
        h = xx(2:end) - xx(1:end-1);
        dx = (yy(2:end) - yy(1:end-1))./ h;
        dxx = ((yy(1:end-2) - yy(2:end-1))./h(1:end-1) - (yy(2:end-1) - yy(3:end))./h(2:end))./((h(1:end-1) + h(2:end))/2);
        [xx' yy' [dxleft; dx(2:end)'; dxright] [dxxleft; dxx'; dxxright]]
        plot(xx', yy', xin', yin', xin', lb(1:n), xin', ub(1:n))
    end
    disp('aa,bb,cc,dd');
    [aa' bb' cc' dd']
    smoothCoeff
    xin
    flatregion
end

    function [xf, fval, re, g, exitflag, flat, leftflat, rightflat] = naiveSearch(flat, leftflat, rightflat, allowchangeconvexconcave)
        %         [f, re1, gg1, exitflag1] = gcv(1e-7);
        %         [f2, re2, gg2, exitflag2] = gcv(2);
        %         [f3, re3, gg3, exitflag3] = gcv(1);
        %
        %         if f < f2
        %             xf = f0;
        %             fval = f;
        %             re = re1;
        %             g = gg1;
        %             exitflag = exitflag1;
        %         else
        %             xf = bx;
        %             fval = f2;
        %             re = re2;
        %             g = gg2;
        %             exitflag = exitflag2;
        %         end
        %
        %         if f3 < fval
        %             xf = (ax + bx)/2;
        %             fval = f3;
        %             re = re3;
        %             g = gg3;
        %             exitflag = exitflag3;
        %         end
        
        if smooth
            xf = fminbnd(@(x) gcv(x, flat, leftflat, rightflat, allowchangeconvexconcave), 0, 20, optimset(optfminbnd, 'TolX', 0.5));
        else
            xf = fminbnd(@(x) gcv(x, flat, leftflat, rightflat, allowchangeconvexconcave), 0, 500, optimset(optfminbnd, 'TolX', 1));
        end
        
        [fval, re, g, exitflag, flat, leftflat, rightflat] = gcv(xf, flat, leftflat, rightflat, allowchangeconvexconcave);
    end


    function [score, re, gg, exitflag,flat, leftflat, rightflat] = gcv(smoothCoeff, flat, leftflat, rightflat, allowchangeconvexconcave)
        flatbf = flat;
        leftflatbf = leftflat;
        rightflatbf = rightflat;
        try
            [re, gg, exitflag, flat, leftflat, rightflat] = calculation(smoothCoeff, lb, lb2, ub, ub2,flat, leftflat, rightflat, allowchangeconvexconcave);
            if (exitflag > 0)
                score = splineHelper.calculateGCV(yin, gg, weight, smoothCoeff, Q, R);
            else
                score = 1e100;
            end
        catch e
            re = nan;
            gg = nan;
            flat = flatbf;
            leftflat = leftflatbf;
            rightflat = rightflatbf;
            if strcmp(e.message, 'Fail to make curve monotone')
                exitflag = -8;
            else
                exitflag = -1;
            end
            score = 1e100;
        end
    end

    function [re, g, exitflag, flat, leftflat, rightflat] = calculation(smoothCoeff, lb, lb2, ub, ub2, flat, leftflat, rightflat, allowchangeconvexconcave)
        %         if isnan(minxrange(1))
        %             ignoremin = true;
        %         else
        ignoremin = false;
%         if isempty(x(x<=minxrange(2) & x>=minxrange(1)))
%             firstafter = x(x>=minxrange(2));
%             if isempty(firstafter)
%                 firstafter = inf;
%             else
%                 firstafter = firstafter(1);
%             end
%             lastbefore = x(x<=minxrange(1));
%             if isempty(lastbefore)
%                 lastbefore = -inf;
%             else
%                 lastbefore = lastbefore(end);
%             end
%         else
%             firstafter = inf;
%             lastbefore = -inf;
%         end
        %         end
        
        minx = xin(yin == min(yin));
        if ~isempty(minx) && length(minx) > 1
            if (leftright == 1 && flat) || (isnan(leftright) && rightflat)
                minx = minx(end);
            elseif (leftright == -1 && flat) || (isnan(leftright) && leftflat);
                minx = minx(1);
            else
                % not flat case might need to consider f'
                minx = minx(ceil(length(minx)/2));
            end
        end
        
        if ~ignoremin &&  minx >= minxrange(2)
            minx = minxrange(2);
            minleftright = -1;
        elseif ~ignoremin &&  minx <= minxrange(1)
            minx = minxrange(1);
            minleftright = 1;
        else
            minx = nan;
            minleftright = 0;
        end
        
        relbub = false;
        if isnan(leftright)
            if (method == 1 && (minx > tpr1 || minx < tpl1)) || (method == 2 && (minx > tpr2 || minx < tpl2))
                relbub = true;
            end
        elseif (leftright == -1 && ~flat) || (leftright == 1 && flat)
            if (method == 1 && tp > minx) || (method == 2 && tp2 > minx)
                relbub = true;
            end
        else
            if (method == 1 && tp < minx) || (method == 2 && tp2 < minx)
                relbub = true;
            end
        end
        
        if (relbub && allowchangeconvexconcave)
            if method == 1
                [lb, ~, ub, ~, flat, leftflat, rightflat] = calculateConcavePoints(originSecD, method, []);
            else
                [~, lb2, ~, ub2, flat, leftflat, rightflat] = calculateConcavePoints(originSecD, method, []);
            end
        end
        
        [xinc, xdec, Aless, bless, Aeqq, Beqq, ubb, ubb2] = dealWithMin(minx, Ales, bles, Aeq, Beq, ub, ub2, minleftright);
        
%         Aless = Ales;
%         bless = bles;
%         Aeqq = Aeq;
%         Beqq = Beq;
%         ubb = ub;
%         ubb2 = ub2;
        [re, g, exitflag] = calculationImp(smoothCoeff, Aless, bless, Aeqq, Beqq, lb, lb2, ubb, ubb2);
        if (exitflag < 1)
            return
        end
        needRefit = true;
        count = 0;
        linesofeq = 0;
        prevminisrange = false;
        
        while exitflag >= 0 && needRefit
            needRefit = false;
            gammaa = re(n+1:end);
            
            %             % for test
            %             xxx = xinc(1);
            %             idxx = find(x == xxx);
            %             idxm1 = idxx - 1;
            %             hii = h(idxm1);
            %             di = (gammaa(idxm1) - gammaa(idxm1-1)) / 6 / hii;
            %             bi = (g(idxm1+1) - g(idxm1))/hii - hii/6*(2*gammaa(idxm1-1)+gammaa(idxm1));
            %             ci = gammaa(idxm1-1)/2;
            %             delta = minx - x(idxm1);
            %             yyy = g(idxm1) + bi * delta + ci * delta * delta + di * delta^3
            % %             dxxx = bi + 2 * ci + delta + 3 * di * delta * delta
            % %             ddxx = 2 * ci + 6 * di * deltaall(diff(g(g<=minx)) <= 0) | ~all(diff(g(g>=minx)) >= 0)
            minx = xin(g == min(g));
            
            if ~isempty(minx) && length(minx) > 1
                if (leftright == 1 && flat) || (isnan(leftright) && rightflat)
                    minx = minx(end);
                elseif (leftright == -1 && flat) || (isnan(leftright) && leftflat);
                    minx = minx(1);
                else
                    % not flat case might need to consider f'
                    minx = minx(ceil(length(minx)/2));
                end
            end
            
            changedMin = false;
            % if count == 0 we always want to make sure minx inside
            % minxrange
            % else if 
            if ~ignoremin && ((count == 0 && (minx > minxrange(2) || (minx == minxrange(2) && minx ~= xin(end)))))
                minx = minxrange(2);
                minleftright = -1;
                needRefit = true;
                changedMin = true;
                prevminisrange = true;
            elseif ~ignoremin && ((count == 0 && (minx < minxrange(1) || (minx == minxrange(1) && minx ~= minxrange(1)))))
                minx = minxrange(1);
                minleftright = 1;
                needRefit = true;
                changedMin = true;
                prevminisrange = true;
            else
                if (count > 0 && prevminisrange)
                   changedMin = true; 
                end
                minleftright = 0;
                prevminisrange = false;
            end
            
            if ~isempty(minincrease)
                if minincrease < minx
                    if (minincrease == xin(1))
                        minx = xin(1);
                        minleftright = 0;
                    else
                        minx = minincrease;
                        minleftright = -1;
                    end
                    changedMin = true;
                    needRefit = true;
                end
            end
            if ~isempty(maxdecrease)
                if (minx <= maxdecrease)
                    if maxdecrease == xin(end)
                        minx = xin(end);
                        minleftright = 0;
                    else
                        minx = maxdecrease;
                        minleftright = 1;
                    end
                    changedMin = true;
                    needRefit = true;
                end
            end
            
            if count == 0 && (minx == xin(end) && yin(end-1) < yin(end))
                minx = xin(yin == min(yin));
                if (length(minx) > 1)
                    minx = minx(end);
                end
                changedMin = true;
                needRefit = true;
                %minleftright = -1;
            end
            if count == 0 && ((minx == xin(1) && yin(2) < yin(1)))
                minx = xin(yin==min(yin));
                if (length(minx) > 1)
                    minx = minx(1);
                end
                changedMin = true;
                needRefit = true;
                %minleftright = 1;
            end
            
            %             if (minx < minxrange(1) || minx > minxrange(2))
            %                 minxrange = [nan; nan];
            %             end
            
            % if not monotone need refit
            if ~needRefit
                if ~all(diff(g(xin<=minx)) <= 0) || ~all(diff(g(xin>=minx)) >= 0)
                    needRefit = true;
                end
            end
            
            relbub = false;
            if isnan(leftright)
                if (method == 1 && (minx > tpr1 || minx < tpl1)) || (method == 2 && (minx > tpr2 || minx < tpl2))
                    relbub = true;
                end
            elseif (leftright == -1 && ~flat) || (leftright == 1 && flat)
                if (method == 1 && tp > minx) || (method == 2 && tp2 > minx)
                    relbub = true;
                end
            else
                if (method == 1 && tp < minx) || (method == 2 && tp2 < minx)
                    relbub = true;
                end
            end

            if (relbub && allowchangeconvexconcave)
                gtmp = g';
                newSecD =((gtmp(3:n) - gtmp(2:n-1))./h(2:end) - (gtmp(2:n-1) - gtmp(1:n-2))./ h(1:end-1))./((h(1:end-1) + h(2:end))/2);
                if method == 1
                    [lb, ~, ub, ~, flat, leftflat, rightflat] = calculateConcavePoints(newSecD, method, []);
                else
                    [~, lb2, ~, ub2, flat, leftflat, rightflat] = calculateConcavePoints(newSecD, method, []);
                end
                changedMin = true;
                needRefit = true;
            end
            
            if changedMin || count == 0 % here if the minx is not good enough we want to force it
                [xinc, xdec, Aless, bless, Aeqq, Beqq, ubb, ubb2] = dealWithMin(minx, Ales, bles, Aeq, Beq, ub, ub2, minleftright);
                needRefit = true;
            end
            
            [re, g, exitflag] = calculationImp(smoothCoeff, Aless, bless, Aeqq, Beqq, lb, lb2, ubb, ubb2);
            
            % check monotone increasing after minx
            if ~changedMin || count == 0
                if ~isempty(xinc)
                    if minleftright == 1 %|| (minleftright == 0 && isnan(minxrange(2)))
                        range = 2 : length(xinc);
                    else
                        range = 1 : length(xinc);
                    end
                    
                    for ii = range
                        xxx = xinc(ii);
                        if (xxx == xin(end)) %ignore last one
                            continue;
                        end
                        idxx = find(xin == xxx);
                        hii = xin(idxx + 1) - xxx;
                        
                        if (xxx == xin(1))
                            if ~isnan(dxxleft)
                                di = (gammaa(1) - dxxleft)/6/h(1);
                                bi = (g(2) - g(1))/h(1) - h(1)/6*(2*dxxleft + gammaa(1));
                                ci = dxxleft/2;
                            else
                                di = gammaa(1)/6/h(1);
                                bi = (g(2) - g(1))/h(1) - h(1)/6*gammaa(1);
                                ci = 0;
                            end
                        elseif (xxx == xin(end-1))
                            if ~isnan(dxxright)
                                di = (dxxright - gammaa(n-2)) /6/h(n-1);
                                bi = (g(n) - g(n-1))/h(n-1) - h(n-1)/6*(2*gammaa(n-2) + dxxright);
                                ci = gammaa(n-2)/2;
                            else
                                di = -gammaa(n-2)/6/h(n-1);
                                bi = (g(n) - g(n-1))/h(n-1) - h(n-1)/3*gammaa(n-2);
                                ci = gammaa(end)/2;
                            end
                        else
                            di = (gammaa(idxx) - gammaa(idxx-1)) / 6 / hii;
                            bi = (g(idxx+1) - g(idxx))/hii - hii/6*(2*gammaa(idxx-1)+gammaa(idxx));
                            ci = gammaa(idxx-1)/2;
                        end
                        
                        deltai = -ci/3/di;
                        % check critical point lies outside the subinterval
                        if deltai <=0 || deltai >= hii;
                            continue;
                        end
                        
                        % we do care if left > right
                        left = g(idxx);
                        otherend = g(idxx) + bi *hii + ci*hii^2 + di*hii^3;
                        % even left <= right we might still have problem
                        if (left <= otherend)
                            %check di <= 0
                            if di <= 0
                                continue;
                            end
                            
                            % if at the extreme not larger than 1e-3 ignore
                            extreme = round((g(idxx) + bi * deltai + ci * deltai^2 + di * deltai^3) * 100000)/100000;
                            if extreme <= round(otherend * 100000)/100000
                                continue;
                            end
                            
                            % determine the sign of the extreme value of g'
                            extremedx = bi - ci*ci/3/di;
                            if extremedx >= 0
                                continue;
                            end
                        end
                        
                        % we are here if we need to add constraint
                        needRefit = true;
                        deltai2 = deltai * deltai;
                        
                        if xxx == xin(1)
                            %b1 + 2ci*deltaj + 3d1*deltaj^2 + (-h1/3 + deltaj - deltaj/2/hii) * dxxleft >= 0
                            Aless = [Aless; zeros(1, idxx-1), 1/hii, -1/hii, zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2), -(-hii/6 + deltai2/2/hii), zeros(1, n-4-(idxx-2))];
                            if isnan(dxxleft)
                                bless = [bless; 0];
                            else
                                bless = [bless; (-hii/3 + deltai - deltai2/2/hii) * dxxleft];
                            end
                            
                            %2ci + 6d1*deltaj + (1 - deltaj/h1)*dxxleft = 0
                            Aeqq = [Aeqq; zeros(1,n), zeros(1,idxx-2), 1/hii, zeros(1,n-4-(idxx-2))];
                            if isnan(dxxleft)
                                Beqq = [Beqq; 0];
                            else
                                Beqq = [Beqq; -(1 - deltai/hii) * dxxleft];
                            end
                        elseif xxx == xin(end-1)
                            Aless = [Aless; zeros(1, idxx-1), 1/hii, -1/hii, zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2), -(-hii/3 + deltai - deltai2/2/hii)];
                            if isnan(dxxright)
                                bless = [bless; 0];
                            else
                                bless = [bless; (-hii/6 + deltai2/2/hii) * dxxright];
                            end
                            
                            Aeqq = [Aeqq; zeros(1, n), zeros(1, idxx -2), (1-deltai/hii)];
                            if isnan(dxxright)
                                Beqq = [Beqq; 0];
                            else
                                Beqq = [Beqq; - 1/hii * dxxright];
                            end
                        else
                            %bj + 2cj*deltaj + 3dj*deltaj^2 >= 0
                            Aless = [Aless; zeros(1, idxx-1), 1/hii, -1/hii, zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2),-(-hii/3 + deltai - deltai2/2/hii), -(-hii/6 + deltai2/2/hii), zeros(1, n-4-(idxx-2))];
                            bless = [bless; 0];
                            
                            %2cj+6dj*deltaj = 0
                            Aeqq = [Aeqq; zeros(1, n), zeros(1, idxx -2), (1-deltai/hii), 1/hii, zeros(1, n-4-(idxx-2))];
                            Beqq = [Beqq; 0];
                        end
                        linesofeq = linesofeq + 1;
                    end
                end
                % check monotone decreasing before minx
                if ~isempty(xdec)
                    if minleftright == -1 %|| (minleftright == 0 && isnan(minxrange(1)))
                        range = 1 : length(xdec) - 1;
                    else
                        range = 1 : length(xdec);
                    end
                    for ii = range
                        xxx = xdec(ii);
                        if (xxx == xin(end)) %ignore last one
                            continue;
                        end
                        idxx = ii;
                        hii = h(idxx);
                        
                        if (xxx == xin(1))
                            if ~isnan(dxxleft)
                                di = (gammaa(1) - dxxleft)/6/h(1);
                                bi = (g(2) - g(1))/h(1) - h(1)/6*(2*dxxleft + gammaa(1));
                                ci = dxxleft/2;
                            else
                                di = gammaa(1)/6/h(1);
                                bi = (g(2) - g(1))/h(1) - h(1)/6*gammaa(1);
                                ci = 0;
                            end
                        elseif (xxx == xin(end-1))
                            if ~isnan(dxxright)
                                di = (dxxright - gammaa(n-2)) /6/h(n-1);
                                bi = (g(n) - g(n-1))/h(n-1) - h(n-1)/6*(2*gammaa(n-2) + dxxright);
                                ci = gammaa(n-2)/2;
                            else
                                di = -gammaa(n-2)/6/h(n-1);
                                bi = (g(n) - g(n-1))/h(n-1) - h(n-1)/3*gammaa(n-2);
                                ci = gammaa(end)/2;
                            end
                        else
                            di = (gammaa(idxx) - gammaa(idxx - 1)) / 6 / hii;
                            bi = (g(idxx+1) - g(idxx))/hii - hii/6*(2*gammaa(idxx-1)+gammaa(idxx));
                            ci = gammaa(idxx-1)/2;
                        end
                        
                        % check critical point lies outside the subinterval
                        deltai = -ci/3/di;
                        if deltai <=0 || deltai >=hii
                            continue;
                        end
                        
                        % we do care if the left < right
                        left = g(idxx);
                        otherend = g(idxx) + bi *hii + ci*hii^2 + di*hii^3;
                        % even left >= otherend, we might still have problem
                        if (left >= otherend)
                            %check di >= 0
                            if di >= 0
                                continue;
                            end
                            
                            % if at the extreme not less than 1e-3 ignore
                            extreme = round((g(idxx) + bi * deltai + ci * deltai^2 + di * deltai^3) * 100000)/100000;
                            if extreme >= round(otherend * 100000)/100000
                                continue;
                            end
                            
                            % determine the sign of the extreme value of g'
                            extremedx = bi - ci*ci/3/di;
                            if extremedx <=0
                                continue;
                            end
                        end
                        
                        % we are here if we need to add constraint
                        needRefit = true;
                        deltai2 = deltai * deltai;
                        if xxx == xin(1)
                            %b1 + 2ci*deltaj + 3d1*deltaj^2 + (-h1/3 + deltaj - deltaj/2/hii) * dxxleft <= 0
                            Aless = [Aless; zeros(1, idxx-1),  -1/hii, 1/hii, zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2), (-hii/6 + deltai2/2/hii), zeros(1, n-4-(idxx-2))];
                            if isnan(dxxleft)
                                bless = [bless; 0];
                            else
                                bless = [bless; -(-hii/3 + deltai - deltai2/2/hii) * dxxleft];
                            end
                            
                            %2ci + 6d1*deltaj + (1 - deltaj/h1)*dxxleft = 0
                            Aeqq = [Aeqq; zeros(1,n),zeros(1,idxx-2), 1/hii, zeros(1,n-4-(idxx-2))];
                            if isnan(dxxleft)
                                Beqq = [Beqq; 0];
                            else
                                Beqq = [Beqq; -(1 - deltai/hii) * dxxleft];
                            end
                        elseif xxx == xin(end-1)
                            Aless = [Aless; zeros(1, idxx-1),  -1/hii, 1/hii, zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2), (-hii/3 + deltai - deltai2/2/hii)];
                            if isnan(dxxright)
                                bless = [bless; 0];
                            else
                                bless = [bless; -(-hii/6 + deltai2/2/hii) * dxxright];
                            end
                            
                            Aeqq = [Aeqq; zeros(1, n), zeros(1, idxx -2), (1-deltai/hii)];
                            if isnan(dxxright)
                                Beqq = [Beqq; 0];
                            else
                                Beqq = [Beqq; - 1/hii * dxxright];
                            end
                        else
                            %bj + 2cj*deltaj + 3dj*deltaj^2 <= 0
                            Aless = [Aless; zeros(1, idxx-1),  -1/hii, 1/hii, zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2), (-hii/3 + deltai - deltai2/2/hii), (-hii/6 + deltai2/2/hii), zeros(1, n-4-(idxx-2))];
                            bless = [bless; 0];
                            
                            %2cj+6dj*deltaj = 0
                            Aeqq = [Aeqq; zeros(1, n), zeros(1, idxx -2), (1-deltai/hii), 1/hii, zeros(1, n-4-(idxx-2))];
                            Beqq = [Beqq; 0];
                        end
                        linesofeq = linesofeq + 1;
                    end
                end
            end
            count = count + 1;
            if (needRefit)
                if count == 4
                    baseException = MException('flexWingFit:monotone', 'Fail to make curve monotone');
                    throw(baseException);
                end
                [re, g, exitflag] = calculationImp(smoothCoeff, Aless, bless, Aeqq, Beqq, lb, lb2, ubb, ubb2);
                if (exitflag < 1)
                    return
                end
                Aeqq = Aeqq(1:end-linesofeq,:);
                Beqq = Beqq(1:end-linesofeq,:);
                linesofeq = 0;
            end
        end
    end

    function [lb, lb2, ub, ub2, flat, leftflat, rightflat, potentials] = calculateConcavePoints(dxx, method, potentials)
        %initialize
        lb = [zeros(n, 1); -inf(n - 2, 1)];
        ub = [inf(n, 1); inf(n - 2, 1)];
        lb2 = [zeros(n, 1); -inf(n - 2, 1)];
        ub2 = [inf(n, 1); inf(n - 2, 1)];
        flat = false;
        
        leftflat = mean(originFirstD(1:min(3, n-2))) > 0 && yin(1) == min(yin);
        rightflat = mean(originFirstD(n-2-min(3,n-2)+1:end)) < 0 && yin(end) == min(yin);
        %         if (isnan(leftright))
if (isempty(potentials))
            if (leftflat) % flat left wing is like a right wing
                if isnan(method) || method == 1
                    concavePointsl1 = 0; tpl1 = xin(1);
                    [concavePointsr1,tpr1, pr] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(2), -1, true, true);
                    if ~isempty(pr)
                        [rowstmp, ~] = size(pr);
                        potentials = [repmat([0 xin(1)], rowstmp, 1) pr];
                    end
                end
                if isnan(method) || method == 2
                    concavePointsl2 = 0; tpl2 = xin(1);
                    [concavePointsr2,tpr2, ~] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(2), -1, true, false);
                end
            elseif (rightflat)% right flat wing is like a left wing
                if isnan(method) || method == 1
                    [concavePointsl1,tpl1, pl] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(1), 1, true, true);
                    concavePointsr1 = 0; tpr1 = xin(end);
                    if ~isempty(pl)
                       [rowstmp, ~] = size(pl);
                       potentials = [pl repmat([0 xin(end)], rowstmp, 1)];
                    end
                end
                if isnan(method) || method == 2
                    [concavePointsl2,tpl2, ~] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(1), 1, true, false);
                    concavePointsr2 = 0; tpr2 = xin(end);
                end
            else % else first left then right
                pl = [];
                pr = [];
                idx = find(xin <= minx);
                xtmp = xin(idx);
                lsecD = dxx(idx(1:end-2));
                if length(xtmp) == 1 || isempty(lsecD)
                    if isnan(method) || method == 1
                        concavePointsl1 = 0;
                        tpl1 = xin(1);
                    end
                    if isnan(method) || method == 2
                        tpl2 = xin(1);
                        concavePointsl2 = 0;
                    end
                else
                    if isnan(method) || method == 1
                        [concavePointsl1, tpl1, pl] =  splineHelper.calConcavePoints(xtmp, length(xtmp), lsecD, turningPoint(1), -1, false, true);
                        %                         if concavePointsl1 == 1
                        %                             concavePointsl1 = 0;
                        %                             tpl1 = x(1);
                        %                         end
                    end
                    if isnan(method) || method == 2
                        [concavePointsl2, tpl2, ~] =  splineHelper.calConcavePoints(xtmp, length(xtmp), lsecD, turningPoint(1), -1, false, false);
                        %                         if (concavePointsl2 == 1)
                        %                             concavePointsl2 = 0;
                        %                             tpl2 = x(1);
                        %                         end
                    end
                end
                
                if isnan(method) || method == 1
                    m = max(tpl1, minx);
                    idx = find(xin>=m);
                    xtmp = xin(idx);
                    rsecD = dxx(idx(1:end-2));
                    if isempty(rsecD)
                        concavePointsr1 = 0;
                        tpr1 = xin(end);
                    else
                        [concavePointsr1,tpr1, pr] = splineHelper.calConcavePoints(xtmp, length(xtmp), rsecD, turningPoint(2), 1, false, true);
                        %                         if concavePointsr1 == 1
                        %                             concavePointsr1 = 0;
                        %                             tpr1 = x(end);
                        %                         end
                    end
                end
                if isnan(method) || method == 2
                    m = max(tpl2, minx);
                    idx = find(xin>=m);
                    xtmp = xin(idx);
                    rsecD = dxx(idx(1:end-2));
                    if isempty(rsecD)
                        concavePointsr2 = 0;
                        tpr2 = xin(end);
                    else
                        [concavePointsr2,tpr2, ~] = splineHelper.calConcavePoints(xtmp, length(xtmp), rsecD, turningPoint(2), 1, false, false);
                        %                         if concavePointsr2 == 1
                        %                             concavePointsr2 = 0;
                        %                             tpr2 = x(end);
                        %                         end
                    end
                end
                
                if ~isempty(pr) || ~isempty(pl)
                    pl = [concavePointsl1 tpl1; pl];
                    pr = [concavePointsr1 tpr1; pr];
                    
                    [rowspr, ~] = size(pr);
                    [rowspl, ~] = size(pl);
                    potentials = zeros(rowspr * rowspl, 4);
                    for rowtmp = 1:1:rowspr
                       potentials(rowspl*(rowtmp-1)+1:rowspl*rowtmp, :) = [pl repmat(pr(rowtmp, :), rowspl, 1)]; 
                    end
                    potentials = potentials(2:end,:); % last row is just normal [concavePointsl1 tpl1 concavePointsr1 tpr1]
                end
                    
            end
            
            if isnan(method) || method == 1
                if dxxleft > 0
                    %changed = changed | concavePointsl1 ~= 0;
                    concavePointsl1 = 0;tpl1 = xin(1);
                    %concavePointsl2 = 0;tpl2 = x(1);
                end
                if dxxright > 0
                    %changed = changed | concavePointsr1 ~= 0;
                    concavePointsr1 = 0;tpr1 = xin(end);
                    %concavePointsr2 = 0;tpr2 = x(end);
                end
            end
             
            flat = leftflat || rightflat;
else
    concavePointsl1 = potentials(1);
    tpl1 = potentials(2);
    concavePointsr1 = potentials(3);
    tpr1 = potentials(4);
    
    concavePointsl2 =0;
    tpl2 = xin(1);
    concavePointsr2 = 0;
    tpr2 = xin(end);
end
            %         concavePoints = -1;
            %         concavePoints2 = -1;
            %         tpl = 0;
            %         tpr = inf;
            %         flat = false;
            %     if (leftflat)
            %         Ales = [Ales; 1/h(1), -1/h(1), zeros(1, n-2), h(1)/6, zeros(1, n-3)];
            %         bles = [bles; 0];
            %     else
            %         Ales = [Ales; -1/h(1), +1/h(1), zeros(1, n-2), -h(1)/6, zeros(1, n-3)];
            %         bles = [bles; -1e-7];
            %     end
            %     if (rightflat)
            %         Ales = [Ales; zeros(1,n-2), -1/h(n-1), +1/h(n-1), zeros(1, n-3), +h(n-1)/6];
            %         bles = [bles; 0];
            %     else
            %         Ales = [Ales; zeros(1,n-2), +1/h(n-1), -1/h(n-1), zeros(1, n-3), -h(n-1)/6];
            %         bles = [bles; -1e-7];
            %     end
            
%         elseif leftright == -1 && mean(originFirstD(1:min(3, n-2))) > 0 && yin(1) == min(yin)
%             % if left wing is monotone increasing which is abnormal, we make left
%             % wing monotone increasing and extrapolate a flat wing
%             flat = true;
%             leftflat = true;
%             rightflat = false;
%             if isnan(method) || method == 1
%                 [concavePoints,tp] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(1), leftright, flat, true);
%                 %                 if concavePoints == 1
%                 %                     concavePoints = 0;
%                 %                 end
%             end
%             if isnan(method) || method == 2
%                 [concavePoints2,tp2] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(1), leftright, flat, false);
%                 %                 if concavePoints == 1
%                 %                     concavePoints = 0;
%                 %                 end
%             end
%             %     %b1 >= 0 (-b1 <= 0)
%             %     Ales = [Ales; 1/h(1), -1/h(1), zeros(1, n-2), h(1)/6, zeros(1, n-3)];
%             %     bles = [bles; 0];
%         elseif leftright == 1 && mean(originFirstD(n-2-min(3,n-2)+1:end)) < 0 && yin(end) == min(yin)
%             % if right wing is monotone decreasing which is abnormal, we make right
%             % wing monotone decreasing and extrapolate a flat wing
%             flat = true;
%             leftflat = false;
%             rightflat = true;
%             if isnan(method) || method == 1
%                 [concavePoints,tp] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(1), leftright, flat, true);
%                 %                 if concavePoints == 1
%                 %                     concavePoints = 0;
%                 %                 end
%             end
%             if isnan(method) || method == 2
%                 [concavePoints2,tp2] = splineHelper.calConcavePoints(xin, n, dxx, turningPoint(1), leftright, flat, false);
%                 %                 if concavePoints == 1
%                 %                     concavePoints = 0;
%                 %                 end
%             end
%             %    %bn <= 0
%             %     Ales = [Ales; zeros(1,n-2), -1/h(n-1), +1/h(n-1), zeros(1, n-3), +h(n-1)/6];
%             %     bles = [bles; 0];
%         else
%             flat = false;
%             leftflat = false;
%             rightflat = false;
%             if leftright == -1
%                 idx = find(xin <= minx);
%                 xtmp = xin(idx);
%                 lsecD = dxx(idx(1:end-2));
%                 if length(xtmp) == 1 || isempty(lsecD)
%                     if isnan(method) || method == 1
%                         concavePoints = 0;
%                         tp = xin(1);
%                     end
%                     if isnan(method) || method == 2
%                         concavePoints2 = 0;
%                         tp2 = xin(1);
%                     end
%                 else
%                     if isnan(method) || method == 1
%                         [concavePoints,tp] = splineHelper.calConcavePoints(xtmp, length(xtmp), lsecD, turningPoint(1), leftright, flat, true);
%                         %                         if concavePoints == 1
%                         %                             concavePoints = 0;
%                         %                             tp = x(1);
%                         %                         end
%                     end
%                     if isnan(method) || method == 2
%                         [concavePoints2,tp2] = splineHelper.calConcavePoints(xtmp, length(xtmp), lsecD, turningPoint(1), leftright, flat, false);
%                         %                         if concavePoints2 == 1
%                         %                             concavePoints2 = 0;
%                         %                             tp2 = x(1);
%                         %                         end
%                     end
%                 end
%             else
%                 idx = find(xin>=minx);
%                 xtmp = xin(idx);
%                 rsecD = dxx(idx(1:end-2));
%                 if isempty(rsecD)
%                     if isnan(method) || method == 1
%                         concavePoints = 0;
%                         tp = xin(end);
%                     end
%                     if isnan(method) || method == 2
%                         concavePoints2 = 0;
%                         tp2 = xin(end);
%                     end
%                 else
%                     if isnan(method) || method == 1
%                         [concavePoints,tp] = splineHelper.calConcavePoints(xtmp, length(xtmp), rsecD, turningPoint(1), leftright, flat, true);
%                         %                         if concavePoints == 1
%                         %                             concavePoints = 0;
%                         %                             tp = x(end);
%                         %                         end
%                     end
%                     if isnan(method) || method == 2
%                         [concavePoints2,tp2] = splineHelper.calConcavePoints(xtmp, length(xtmp), rsecD, turningPoint(1), leftright, flat, false);
%                         %                         if concavePoints2 == 1
%                         %                             concavePoints2 = 0;
%                         %                             tp2 = x(end);
%                         %                         end
%                     end
%                 end
%             end
%             
%             %     % Ales/bles, make sure strictly monotone at the end by setting leftwing b1 < -1e-7 and bn > 1e-7 (-bn < -1e-7)
%             %     if leftright == -1
%             %         Ales = [Ales; -1/h(1), +1/h(1), zeros(1, n-2), -h(1)/6, zeros(1, n-3)];%; zeros(1,n-2), 1/h(n-1), -1/h(n-1), zeros(1, n-3), -h(n-1)/6];
%             %     else
%             %         Ales = [Ales; zeros(1,n-2), +1/h(n-1), -1/h(n-1), zeros(1, n-3), -h(n-1)/6];
%             %     end
%             %     bles = [bles; -1e-7];
%             if isnan(method) || method == 1
%                 if leftright == -1
%                     if dxxleft > 0
%                         changed = changed | concavePoints ~= 0;
%                         concavePoints = 0;tp = xin(1);
%                         %concavePoints2 = 0;tp2 = x(1);
%                     end
%                 else
%                     if dxxright > 0
%                         changed = changed | concavePoints ~= 0;
%                         concavePoints = 0;tp = xin(end);
%                         %concavePoints2 = 0;tp2 = x(end);
%                     end
%                 end
%             end
%         end
        
        % make sure convex/concave
        if (careConvexConcave)
            if (isnan(leftright))
                if changed
                    if isnan(method) || method == 1
                        lb = [zeros(n, 1); -inf(concavePointsl1, 1); -6e-4 * ones(n - 2 - concavePointsl1 - concavePointsr1, 1); -inf(concavePointsr1, 1)];
                        ub = [inf(n, 1); 6e-4 * ones(concavePointsl1, 1); inf(n-2 - concavePointsl1 - concavePointsr1, 1); 6e-4 * ones(concavePointsr1, 1)];
                    end
                    if isnan(method) || method == 2
                        lb2 = [zeros(n, 1); -inf(concavePointsl2, 1); -6e-4 * ones(n - 2 - concavePointsl2 - concavePointsr2, 1); -inf(concavePointsr2, 1)];
                        ub2 = [inf(n, 1); 6e-4 * ones(concavePointsl2, 1); inf(n-2 - concavePointsl2 - concavePointsr2, 1); 6e-4 * ones(concavePointsr2, 1)];
                    end
                else
                    if isnan(method) || method == 1
                        lb = [zeros(n, 1); -inf(concavePointsl1, 1); zeros(n - 2 - concavePointsl1 - concavePointsr1, 1); -inf(concavePointsr1, 1)];
                        ub = [inf(n, 1); zeros(concavePointsl1, 1); inf(n-2 - concavePointsl1 - concavePointsr1, 1); zeros(concavePointsr1, 1)];
                    end
                    if isnan(method) || method == 2
                        lb2 = [zeros(n, 1); -inf(concavePointsl2, 1); zeros(n - 2 - concavePointsl2 - concavePointsr2, 1); -inf(concavePointsr2, 1)];
                        ub2 = [inf(n, 1); zeros(concavePointsl2, 1); inf(n-2 - concavePointsl2 - concavePointsr2, 1); zeros(concavePointsr2, 1)];
                    end
                end
%             elseif (leftright == -1 && ~flat) || (leftright == 1 && flat)
%                 if changed
%                     if isnan(method) || method == 1
%                         lb = vertcat(zeros(n, 1), -inf(concavePoints, 1), -6e-4 * ones(n - 2 - concavePoints, 1));
%                         ub = vertcat(inf(n, 1), 6e-4 * ones(concavePoints, 1), inf(n-2 - concavePoints, 1));
%                     end
%                     if isnan(method) || method == 2
%                         lb2 = vertcat(zeros(n, 1), -inf(concavePoints2, 1), -6e-4 * ones(n - 2 - concavePoints2, 1));
%                         ub2 = vertcat(inf(n, 1), 6e-4 * ones(concavePoints2, 1), inf(n-2 - concavePoints2, 1));
%                     end
%                 else
%                     if isnan(method) || method == 1
%                         lb = [zeros(n, 1); -inf(concavePoints, 1); zeros(n - 2 - concavePoints, 1)];
%                         ub = [inf(n, 1); zeros(concavePoints, 1); inf(n-2 - concavePoints, 1)];
%                     end
%                     if isnan(method) || method == 2
%                         lb2 = [zeros(n, 1); -inf(concavePoints2, 1); zeros(n - 2 - concavePoints2, 1)];
%                         ub2 = [inf(n, 1); zeros(concavePoints2, 1); inf(n-2 - concavePoints2, 1)];
%                     end
%                 end
%             else
%                 if changed
%                     if isnan(method) || method == 1
%                         lb = vertcat(zeros(n, 1), -6e-4 * ones(n-2 - concavePoints, 1), -inf(concavePoints, 1));
%                         ub = vertcat(inf(n, 1), inf(n-2-concavePoints,1), 6e-4 * ones(concavePoints,1));
%                     end
%                     if isnan(method) || method == 2
%                         lb2 = vertcat(zeros(n, 1), -6e-4 * ones(n-2 - concavePoints2, 1), -inf(concavePoints2, 1));
%                         ub2 = vertcat(inf(n, 1), inf(n-2-concavePoints2,1), 6e-4 * ones(concavePoints2,1));
%                     end
%                 else
%                     if isnan(method) || method == 1
%                         lb = [zeros(n, 1); zeros(n-2 - concavePoints, 1); -inf(concavePoints, 1)];
%                         ub = [inf(n, 1); inf(n-2-concavePoints,1); zeros(concavePoints,1)];
%                     end
%                     if isnan(method) || method == 2
%                         lb2 = [zeros(n, 1); zeros(n-2 - concavePoints2, 1); -inf(concavePoints2, 1)];
%                         ub2 = [inf(n, 1); inf(n-2-concavePoints2,1); zeros(concavePoints2,1)];
%                     end
%                 end
            end
        end
        
        if ~isempty(xinlb)
            if isnan(method) || method == 1
                lb(1:n) = xinlb';
            end
            if isnan(method) || method == 2
                lb2(1:n) = xinlb';
            end
        end
        if ~isempty(xinub)
            if isnan(method) || method == 1
                ub(1:n) = xinub';
            end
            if isnan(method) || method == 2
                ub2(1:n) = xinub';
            end
        end
        %         if allowignorelbub && ~isempty(tightlb) % we might be able to cross the very tight market
        %             if isnan(method) || method == 1
        %                 lb(tightlb == 1) = lowerLimitGmin(tightlb == 1);
        %             end
        %             if isnan(method) || method == 2
        %                 lb2(tightlb == 1) =lowerLimitGmin(tightlb == 1);
        %             end
        %         end
        %         if allowignorelbub && ~isempty(tightub)
        %             if isnan(method) || method == 1
        %                 ub(tightub == 1) = upperLimitGmax(tightub == 1);
        %             end
        %             if isnan(method) || method == 2
        %                 ub2(tightub == 1) = upperLimitGmax(tightub == 1);
        %             end
        %         end
        
        % if we force close, under certain circumstances we allow the curve to be a little not convex concave
        if (forceclose)
            %             % if goal as range of large dxx change, we allow it to be not convex concave
            %             if ~isempty(hugedxxregion) && smooth == 2
            %                 hdr = hugedxxregion + n;
            %                 if isnan(method) || method == 1
            %                     lb(hdr) = -inf;
            %                     ub(hdr) = inf;
            %                 end
            %                 if isnan(method) || method == 2
            %                     lb2(hdr) = -inf;
            %                     ub2(hdr) = inf;
            %                 end
            %             end
            
            % if market is very tight some where we allow the curve to be a little not convex concave
            tightidx = n + tight;
            if isnan(method) || method == 1
                tightidxl = find(lb(tightidx) == 0);
                tightidxu = find(ub(tightidx) == 0);
                if ~isempty(tightidxl)
                    lb(tightidx(tightidxl)) = -6e-4;
                end
                if ~isempty(tightidxu)
                    ub(tightidx(tightidxu)) = 6e-4;
                end
            end
            if isnan(method) || method == 2
                tightidxl = find(lb2(tightidx) == 0);
                tightidxu = find(ub2(tightidx) == 0);
                if ~isempty(tightidxl)
                    lb2(tightidx(tightidxl)) = -6e-4;
                end
                if ~isempty(tightidxu)
                    ub2(tightidx(tightidxu)) = 6e-4;
                end
            end
        end
    end

    function [xinc, xdec, Ales, bles, Aeq, Beq, ub, ub2] = dealWithMin(minx, Ales, bles, Aeq, Beq, ub, ub2, minleftright)
        if ~isinf(leftincrease) && leftincrease > minx
            minx = leftincrease;
        end
        if ~isinf(rightincrease) && rightincrease < minx
            minx = rightincrease;
        end
        
        % inc part simple constraint
        if isnan(minx)
            xinc = [];
        else
            if minleftright ~= 0 && ~isnan(minxrange(2))
                xinc = xin(xin>=minxrange(2));
            elseif minleftright == -1
                xinc = xin(xin>=minx);
            else
                xinc = xin(xin>minx);
            end
            %             if (length(xinc) == 1)
            %                 xinc = [];
            %             end
        end
        if minleftright == 1 %|| (minleftright == 0 && isnan(minxrange(2)))
            range = 2:length(xinc);
        else
            range = 1:length(xinc);
        end
        for ii = range
            xx = xinc(ii);
            idxx = find(xin == xx);
            if xx == xin(end)
                % normal right wing end, bn + dxxright*hi/3 > 1e-7, -bn < -1e-7 + dxxright * hi/3
                Ales = [Ales; zeros(1, n-2), 1/h(n-1), -1/h(n-1), zeros(1, n-3), -h(n-1)/6];
                if isnan(dxxright)
                    bles = [bles; -1e-5];
                else
                    bles = [bles; -1e-5 + dxxright * h(n-1)/3];
                end
            elseif xx == xin(1)
                % (this might include leftflat so we allow 0) b1 - dxxleft*h1/3>= 0 (-b1 <= dxxleft*h1/3)
                Ales = [Ales; 1/h(1), -1/h(1), zeros(1, n-2), h(1)/6, zeros(1, n-3)];
                if isnan(dxxleft)
                    bles = [bles; 0];
                else
                    bles = [bles; dxxleft * h(1)/3];
                end
            elseif xx == xin(end-1)
                % bn-1  - dxxright*hn-1/6 > 1e-7, -bn-1 <= -1e-7 - dxxright*hn-1/6
                Ales = [Ales; zeros(1, n-2), 1/h(n-1), -1/h(n-1), zeros(1,n-3), +h(n-1)/3];
                if isnan(dxxright)
                    bles = [bles; -1e-5];
                else
                    bles = [bles; -1e-5 - dxxright * h(n-1)/6];
                end
            else
                % in increase part bi > 1e-7, -bi <= -1e-7
                Ales = [Ales; zeros(1, idxx-1),  1/h(idxx), -1/h(idxx), zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2), h(idxx)/3, h(idxx)/6, zeros(1, n-4-(idxx-2))];
                bles = [bles; -1e-5];
            end
        end
        % dec part simple constraint
        if isnan(minx)
            xdec = [];
        else
            if minleftright ~= 0 && ~isnan(minxrange(1))
                xdec = xin(xin<=minxrange(1));
            elseif minleftright == 1 
                xdec = xin(xin<=minx);
            else
                xdec = xin(xin<minx);
            end
            %             if (length(xdec) == 1)
            %                 xdec = [];
            %             end
        end
        if minleftright == -1 %|| (minleftright == 0 && isnan(minxrange(1)))
            range = 1:length(xdec) - 1;
        else
            range = 1:length(xdec);
        end
        for ii = range
            xx = xin(ii);
            idxx = ii;
            if xx == xin(1)
                % normal left wing start, b1 - dxxleft*h1/3 < -1e-7
                Ales = [Ales; -1/h(1), +1/h(1), zeros(1, n-2), -h(1)/6, zeros(1, n-3)];
                if isnan(dxxleft)
                    bles = [bles; -1e-5];
                else
                    bles = [bles; -1e-5 + dxxleft*h(1)/3];
                end
            elseif xx == xin(end)
                % (this might include rightflat so we allow 0) bn + dxxright *hn-1/3<=0,
                % bn <= -dxxright * hi/3
                Ales = [Ales; zeros(1,n-2), -1/h(n-1), 1/h(n-1), zeros(1, n-3), h(n-1)/6];
                if isnan(dxxright)
                    bles = [bles; 0];
                else
                    bles = [bles; -dxxright * h(n-1)/3];
                end
            elseif xx == xin(end-1)
                % bn-1 < -1e-7 + dxxright * hi/6
                Ales = [Ales; zeros(1, n-2), -1/h(n-1), 1/h(n-1),zeros(1,n-3), -h(n-1)/3];
                if isnan(dxxright)
                    bles = [bles; -1e-5];
                else
                    bles = [bles; -1e-5 + dxxright * h(n-1)/6];
                end
            else
                % in decrease part bi < -1e-7
                Ales = [Ales; zeros(1, idxx-1), -1/h(idxx), 1/h(idxx), zeros(1, n-2-(idxx-1)), zeros(1, idxx - 2), -h(idxx)/3, -h(idxx)/6, zeros(1, n-4-(idxx-2))];
                bles = [bles; -1e-5];
            end
        end
        
        %         % minx point should have positive c, b should be depending on
        %         % minxleftright so that if real min is left of this one, should be
        %         % positive, else negative
        %         if ismember(minx, x)
        %             idx = find(x == minx);
        %             % in case we might keep decreasing in left we don't add constraint
        %             %     if (idx == 1) % should not be worried abt the first one b/c we have
        %             %     boundarydx and boundarydxx
        %             %         % b0 = 0
        %             %         Aeq = [Aeq; -1/h(1), 1/h(1), zeros(1,n-2), -h(1)/6, zeros(1, n-3)];
        %             %         Beq = [Beq; 0];
        %             if (idx ~= length(x) && idx ~= 1) % first or last item is minx then do nothing b/c it might be exactly the min or keep decreasing
        %                 if ~isnan(minxleftright)
        %                     if (minxleftright == 2) % exact here
        %                         % bi = 0
        %                         if idx == length(x) - 1 % gamma(n) = 0
        %                             if isnan(dxxright)
        %                                 Aeq = [Aeq; zeros(1, idx-1), -1/h(idx), 1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx-2), -h(idx)/3, zeros(1, n-3-(idx-2))];
        %                                 Beq = [Beq; 0];
        %                             else % gamma(n) = dxxright => bi*g -dxxright*hi/6 = 0
        %                                 Aeq = [Aeq; zeros(1, idx-1), -1/h(idx), 1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx-2), -h(idx)/3, zeros(1, n-3-(idx-2))];
        %                                 Beq = [Beq; dxxright * h(idx) / 6];
        %                             end
        %                         else
        %                             Aeq = [Aeq; zeros(1, idx -1), -1/h(idx), 1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx -2), -h(idx)/3, -h(idx)/6, zeros(1, n-4-(idx-2))];
        %                             Beq = [Beq; 0];
        %                         end
        %                     elseif (minxleftright == -1) % should be left of this
        %                         %bi > 0
        %                         if idx == length(x) - 1 % gamma(n) = 0
        %                             if isnan(dxxright)
        %                                 Ales = [Ales; zeros(1, idx-1), 1/h(idx), -1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx-2), h(idx)/3, zeros(1, n-3-(idx-2))];
        %                                 bles = [bles; 0];
        %                             else % gamma(n) = dxxright => bi*g -dxxright*hi/6 = 0
        %                                 Ales = [Ales; zeros(1, idx-1), 1/h(idx), -1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx-2), h(idx)/3, zeros(1, n-3-(idx-2))];
        %                                 bles = [bles; dxxright * h(idx) / 6];
        %                             end
        %                         else
        %                             Ales = [Ales; zeros(1, idx -1), 1/h(idx), -1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx -2), h(idx)/3, h(idx)/6, zeros(1, n-4-(idx-2))];
        %                             bles = [bles; 0];
        %                         end
        %                     else % should be right of this
        %                         %bi < 0
        %                         if idx == length(x) - 1 % gamma(n) = 0
        %                             if isnan(dxxright)
        %                                 Ales = [Ales; zeros(1, idx-1), -1/h(idx), 1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx-2), -h(idx)/3, zeros(1, n-3-(idx-2))];
        %                                 bles = [bles; 0];
        %                             else % gamma(n) = dxxright => bi*g -dxxright*hi/6 = 0
        %                                 Ales = [Ales; zeros(1, idx-1), -1/h(idx), 1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx-2), -h(idx)/3, zeros(1, n-3-(idx-2))];
        %                                 bles = [bles; dxxright * h(idx) / 6];
        %                             end
        %                         else
        %                             Ales = [Ales; zeros(1, idx -1), -1/h(idx), 1/h(idx), zeros(1, n-2-(idx-1)), zeros(1, idx -2), -h(idx)/3, -h(idx)/6, zeros(1, n-4-(idx-2))];
        %                             bles = [bles; 0];
        %                         end
        %                     end
        %                 end
        %                 % ci >= 0, -ci <= 0
        %                 Ales = [Ales; zeros(1, n), zeros(1, idx-2), -0.5, zeros(1, n-3 -(idx-2))];
        %                 bles = [bles; 0];
        %             end
        %
        %             %     if ~((idx == 1 && ((isnan(leftright) && ~leftflat) || (leftright == -1 && ~flat) || (leftright == 1))) || (idx == length(x) && ((isnan(leftright) && ~rightflat) || (leftright == -1) || (leftright == 1 && ~flat))))
        %             %
        %             %     end
        %         else
        %             try
        %                 xp1idx = find(x>minx,1);
        %                 xm1idx = xp1idx - 1;
        %                 xp1 = x(xp1idx);
        %                 xm1 = x(xm1idx);
        %                 delta = minx - xm1;
        %                 delta2 = delta * delta;
        %                 hi = xp1-xm1;
        %
        %                 if (minxleftright  == -1) % should be left of this
        %                     %bj + 2cj*deltaj + 3dj*deltaj^2 > 0
        %                     Ales = [Ales; zeros(1, xm1idx-1),  1/hi, -1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, xm1idx - 2), -(-hi/3 + delta - delta2/2/hi), -(-hi/6 + delta2/2/hi), zeros(1, n-4-(xm1idx-2))];
        %                     bles = [bles; 0];
        %                 else
        %                     %bj + 2cj*deltaj + 3dj*deltaj^2 < 0
        %                     Ales = [Ales; zeros(1, xm1idx-1),  -1/hi, 1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, xm1idx - 2), (-hi/3 + delta - delta2/2/hi), (-hi/6 + delta2/2/hi), zeros(1, n-4-(xm1idx-2))];
        %                     bles = [bles; 0];
        %                 end
        %
        %                 %2cj+6dj*deltaj > 0
        %                 Ales = [Ales; zeros(1, n), zeros(1, xm1idx -2), -(1-delta/hi), -1/hi, zeros(1, n-4-(xm1idx-2))];
        %                 bles = [bles; 0];
        %             catch e
        %                 error('error in set min point: ' + e.message);
        %             end
        %         end
        
        if minleftright ~= 0 %&& (~isempty(maxdecrease) && minx ~= maxdecrease) && (~isempty(minincrease) && minx ~= minincrease)
            try
                xp1idx = find(xin>minx,1);
                xm1idx = xp1idx - 1;
                xp1 = xin(xp1idx);
                xm1 = xin(xm1idx);
                delta = minx - xm1;
                delta2 = delta * delta;
                hi = xp1-xm1;
                
                if (minleftright  == -1) % should be left of this
                    %bj + 2cj*deltaj + 3dj*deltaj^2 > 0
                    if (xp1 == xin(2))
                        Ales = [Ales; 1/hi, -1/hi, zeros(1, n-2), -(-hi/6 + delta2/2/hi), zeros(1, n-3)];
                        if ~isnan(dxxleft)
                            bles = [bles; (-hi/3 + delta - delta2/2/hi) * dxxleft];
                        else
                            bles = [bles; 0];
                        end
                    elseif (xp1 == xin(end))
                        Ales = [Ales; zeros(1, xm1idx-1),  1/hi, -1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, n-3), -(hi/6)];
                        if ~isnan(dxxright)
                            bles = [bles; (hi/3 + delta - delta2/2/hi) * dxxright];
                        else
                            bles = [bles; 0];
                        end
                    elseif (xp1 == xin(end - 1))
                        Ales = [Ales; zeros(1, xm1idx-1),  1/hi, -1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, xm1idx - 1), -(-hi/3 + delta - delta2/2/hi)];
                        if ~isnan(dxxright)
                            bles = [bles; (-hi/6 + delta2/2/hi) * dxxright];
                        else
                            bles = [bles; 0];
                        end
                    else
                        Ales = [Ales; zeros(1, xm1idx-1),  1/hi, -1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, xm1idx - 2), -(-hi/3 + delta - delta2/2/hi), -(-hi/6 + delta2/2/hi), zeros(1, n-4-(xm1idx-2))];
                        bles = [bles; 0];
                    end
                else
                    %bj + 2cj*deltaj + 3dj*deltaj^2 < 0
                    if (xp1 == xin(2))
                        Ales = [Ales; -1/hi, 1/hi, zeros(1, n-2), (-hi/6 + delta2/2/hi), zeros(1, n-3)];
                        if ~isnan(dxxleft)
                            bles = [bles; -(-hi/3 + delta - delta2/2/hi) * dxxleft];
                        else
                            bles = [bles; 0];
                        end
                    elseif (xp1 == xin(end))
                        Ales = [Ales; zeros(1, xm1idx-1), -1/hi, 1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, n-3), (hi/6)];
                        if ~isnan(dxxright)
                            bles = [bles; -(hi/3 + delta - delta2/2/hi) * dxxright];
                        else
                            bles = [bles; 0];
                        end
                    elseif (xp1 == xin(end - 1))
                        Ales = [Ales; zeros(1, xm1idx-1),  -1/hi, 1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, xm1idx - 1), (-hi/3 + delta - delta2/2/hi)];
                        if ~isnan(dxxright)
                            bles = [bles; -(-hi/6 + delta2/2/hi) * dxxright];
                        else
                            bles = [bles; 0];
                        end
                    else
                        Ales = [Ales; zeros(1, xm1idx-1),  -1/hi, 1/hi, zeros(1, n-2-(xm1idx-1)), zeros(1, xm1idx - 2), (-hi/3 + delta - delta2/2/hi), (-hi/6 + delta2/2/hi), zeros(1, n-4-(xm1idx-2))];
                        bles = [bles; 0];
                    end
                end
                
                %2cj+6dj*deltaj > 0
                if (xp1 == xin(2))
                    Ales = [Ales; zeros(1, n), -(delta/hi), zeros(1, n-3)];
                    if ~isnan(dxxleft)
                        bles = [bles; (1-delta/hi) * dxxleft];
                    else
                        bles = [bles; 0];
                    end
                elseif (xp1 == xin(end))
                    %nothing
                elseif (xp1 == xin(end - 1))
                    Ales = [Ales; zeros(1, n), zeros(1, xm1idx -1), -(1-delta/hi)];
                    if ~isnan(dxxright)
                        bles = [bles; delta/hi * dxxright];
                    else
                        bles = [bles; 0];
                    end
                else
                    Ales = [Ales; zeros(1, n), zeros(1, xm1idx -2), -(1-delta/hi), -1/hi, zeros(1, n-4-(xm1idx-2))];
                    bles = [bles; 0];
                end
            catch
                % at here if minx is leftmost or minx is rightmost
            end
        end
        
        
        % add the inside boundary if exists (when there are invalid data points
        % between valid points, we dont have a goal but we do have boundary)
        if ~isempty(invalidx)
            for ii = 1:length(invalidx)
                tt = invalidx(ii);
                tupper = invalidupper(ii);
                tlower = invalidlower(ii);
                try
                    tp1idx = find(xin>tt,1);
                    tiidx = tp1idx - 1;
                    tp1 = xin(tp1idx);
                    ti = xin(tiidx);
                catch e
                    % we should not be here b/c the first or last should be valid
                    % or not in invalidx but iend
                    baseException = MException('flexWingFit:badData', 'tt should not be the first or last');
                    ee = addCause(baseException, e);
                    throw(ee);
                end
                if ~isnan(minx)
                    if ti > minx
                        % make sure all implied upper bounds after minx are not smaller
                        % than previous lower bounds(no goals)/goals, and lower bounds are not larger than the
                        % following upper bounds(no goals)/goals
                        leftmaxlowerinvalid =  max(invalidlower(invalidx < tt & invalidx > minx));
                        if isempty(leftmaxlowerinvalid)
                            upperleftinvalid = false;
                        else
                            upperleftinvalid = tupper < leftmaxlowerinvalid;
                        end
                        leftmaxlowervalid = max(xinlb(xin > minx & xin < ti));
                        if isempty(leftmaxlowervalid)
                            upperleftvalid = false;
                        else
                            upperleftvalid = tupper < leftmaxlowervalid;
                        end
                        rightminupperinvalid = min( invalidupper(invalidx > tt));
                        if isempty(rightminupperinvalid)
                            lowerrightinvalid = false;
                        else
                            lowerrightinvalid = tlower > rightminupperinvalid;
                        end
                        rightminuppervalid = min(xinub(tp1idx:end));
                        if isempty(rightminuppervalid)
                            lowerrightvalid = false;
                        else
                            lowerrightvalid = tlower > rightminuppervalid;
                        end
                        if upperleftvalid || upperleftinvalid || lowerrightvalid || lowerrightinvalid
                            err = 'Fail to interpolate with given boundary';
                            error('error in quadprog: %s', err);
                        end
                    elseif ti < minx
                        % make sure all implied lower bounds before minx are not
                        % larger than previous upper bounds(no goals)/goals, and upper
                        % bounds are not smaller than the following lower bounds(no
                        % goals)/goals
                        leftminupperinvalid = min(invalidupper(invalidx<tt));
                        if isempty(leftminupperinvalid)
                            lowerleftinvalid = false;
                        else
                            lowerleftinvalid = tlower > leftminupperinvalid;
                        end
                        leftminuppervalid = min(xinub(1:tiidx));
                        if isempty(leftminuppervalid)
                            lowerleftvalid = false;
                        else
                            lowerleftvalid = tlower > leftminuppervalid;
                        end
                        rightmaxlowerinvalid = max(invalidlower(invalidx>tt & invalidx < minx));
                        if isempty(rightmaxlowerinvalid)
                            upperrightinvalid = false;
                        else
                            upperrightinvalid = tupper < rightmaxlowerinvalid;
                        end
                        rightmaxlowervalid =  max(xinlb(xin > tp1 & xin < minx));
                        if isempty(rightmaxlowervalid)
                            upperrightvalid = false;
                        else
                            upperrightvalid = tupper < rightmaxlowervalid;
                        end
                        if lowerleftvalid || lowerleftinvalid || upperrightvalid || upperrightinvalid
                            err = 'Fail to interpolate with given boundary';
                            error('error in quadprog: %s', err);
                        end
                    end
                end
                hh = tp1 - ti;
                if (tp1 == xin(end))
                    if leftright == -1 && ~isnan(dxxright) % if nonzero boundary second dx
                        if ~isinf(tupper)
                            Ales = [Ales; zeros(1, tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1, n-tp1idx), zeros(1,tiidx-2), -(tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6];
                            bles = [bles; tupper + (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6 * dxxright];
                        end
                        if ~isinf(tlower)
                            Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), zeros(1,tiidx-2), (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6];
                            bles = [bles; -tlower - (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6 * dxxright];
                        end
                    else
                        if ~isinf(tupper)
                            Ales = [Ales; zeros(1, tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1, n-tp1idx), zeros(1,tiidx-2), -(tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6];
                            bles = [bles; tupper];
                        end
                        if ~isinf(tlower)
                            Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), zeros(1,tiidx-2), (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6];
                            bles = [bles; -tlower];
                        end
                    end
                elseif (ti == xin(1))
                    if leftright == 1 && ~isnan(dxxleft) % if nonzero boundary second dx
                        if ~isinf(tupper)
                            Ales = [Ales; zeros(1,tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1,n-tp1idx), -(tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                            bles = [bles; tupper + (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6 * dxxleft];
                        end
                        if ~isinf(tlower)
                            Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                            bles = [bles; -tlower - (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6 * dxxleft];
                        end
                    else
                        if ~isinf(tupper)
                            Ales = [Ales; zeros(1,tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1,n-tp1idx), -(tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                            bles = [bles; tupper];
                        end
                        if ~isinf(tlower)
                            Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                            bles = [bles; -tlower];
                        end
                    end
                else % normal
                    %upper
                    if ~isinf(tupper)
                        Ales = [Ales; zeros(1,tiidx-1), (tp1 - tt)/hh, (tt - ti)/hh, zeros(1,n-tp1idx), zeros(1,tiidx-2), -(tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6, -(tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                        bles = [bles; tupper];
                    end
                    %lower
                    if ~isinf(tlower)
                        Ales = [Ales; zeros(1,tiidx-1), -(tp1 - tt)/hh, -(tt - ti)/hh, zeros(1,n-tp1idx), zeros(1,tiidx-2), (tt-ti)*(tp1-tt)*(1+(tp1-tt)/hh)/6, (tt-ti)*(tp1-tt)*(1+(tt-ti)/hh)/6, zeros(1,n-1-tp1idx)];
                        bles = [bles; -tlower];
                    end
                end
            end
        end
        
        % boundary lb/ub should consider ex part boundary if minx is here
        if ~isnan(minx)
            if minx ~= xin(1) % if letf part is decreasing, leftmost node cannot be greater than the upper bound of left extrapolate
                if ~isempty(ubendl)
                    ub(1)= min(ub(1), min(ubendl));
                    ub2(1) = min(ub2(1), min(ubendl));
                end
            end
            if minx ~= xin(end) % if right part is increasing, rightmost node cannot be greater than the upper bound of the right extrapolate
                if ~isempty(ubendr)
                    ub(n) = min(ub(n), min(ubendr));
                    ub2(n) = min(ub2(n), min(ubendr));
                end
            end
        end
        
        %         % minx region should be convex
        %         if ~isnan(minx)
        %             % don't need to worry abt really tight ones
        %             if tight > 0.03 && ~isempty(xinc)
        %                 concavableidx = find(x>=xinc(2),1);
        %                 tmp = lb(n+1:end);
        %                 if (tight <= 0.05);
        %                     lastconvexidx = find(tmp==-6e-4, 1, 'last');
        %                 else
        %                     lastconvexidx = find(tmp==0, 1, 'last');
        %                 end
        %                 if (lastconvexidx < concavableidx)
        %                     if (tight <= 0.05)
        %                         lb(n+1+lastconvexidx:n+1+concavableidx - 1) = -6e-4;
        %                     else
        %                         lb(n+1+lastconvexidx:n+1+concavableidx - 1) = 0;
        %                     end
        %                     ub(n+1+lastconvexidx:n+1+concavableidx - 1) = inf;
        %                 end
        %             end
        %         end
    end

    function [re, g, exitflag] = calculationImp(smoothCoeff, Ales, bles, Aeq, Beq, lb, lb2, ub, ub2)
        D = blkdiag(diag(weight), smoothCoeff*R);
        eta = [weight.*yin zeros(1, n-2)]';
%                 D = blkdiag(diag(ones(1,n)), smoothCoeff*R);
%                 eta = [y zeros(1, n-2)]';
        if ~isnan(dxxleft)
            eta = eta + [zeros(1, n-2) (-0.5 * smoothCoeff * dxxleft/h(end)) (0.5 * smoothCoeff * dxxleft/h(end)) zeros(1, n-2)]';
        end
        if ~isnan(dxxright)
            eta = eta + [(0.5 * smoothCoeff * dxxright/h(1)) (-0.5 * smoothCoeff * dxxright/h(1)) zeros(1, 2 * n-4)]';
        end

        if smoothCoeff == 0
            [re,~,exitflag]= quadprog(D, -eta, [], [], Aeq, Beq, [], [], [], optquad);
        else
            if method == 1
                [re,~,exitflag]= quadprog(D, -eta, Ales, bles, Aeq, Beq, lb, ub, [], optquad);
            else
                [re,~,exitflag]= quadprog(D, -eta, Ales, bles, Aeq, Beq, lb2, ub2, [], optquad);
            end
        end
        if (exitflag > 0)
            g = re(1:n);
        else
            g = [];
        end
    end

    function [increaseregion, decreaseregion] = findIDRegion(lowerLimitG, upperLimitG)
        decreaseregion = [];
        for idx = length(lowerLimitG)-1 : -1 : 1
            l = lowerLimitG(idx);
            for idxx = idx+1:1:length(upperLimitG)
                u = upperLimitG(idxx);
                if (u < l)
                    decreaseregion = idx;
                    break;
                end
            end
            if ~isempty(decreaseregion)
                break;
            end
        end
        
        increaseregion = [];
        for idx = 2: 1: length(lowerLimitG)
            l = lowerLimitG(idx);
            for idxx = 1 : 1: idx-1
               u = upperLimitG(idxx);
               if (u < l)
                   increaseregion = idx;
                   break;
               end
            end
            if ~isempty(increaseregion)
                break;
            end
        end
            
%         increaseregion = find(upperLimitG(1:end-1) < lowerLimitG(2:end));
%         if ~isempty(increaseregion)
%             increaseregion = increaseregion(1);
%         end
    end
end