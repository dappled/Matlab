% exitflag: 3 means one side flat
%           4 means left side flat
%           5 means right side flat
%           6 means both sides flat
function [smoothCoeff1, exitflag, g, gamma, aa, bb, cc, dd, turningPoint, x] = flexTimeFit(xin, yin, w, stationarypoint, tailConcavity, xinlb, xinub, invalidx, invalidupper, invalidlower, smoothCoeff, boundaryx, boundarydx, boundarydxx, leftright, xexl, yexl, xendl, lbendl,ubendl,xexr,yexr, xendr, lbendr, ubendr, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange)
% %input
% clc
% M = csvread('c:\temp\voltooltest\slice_IWM.USZ_20150206.csv', 1);
% leftright = nan;
% smoothCoeff = nan;
% stationarypoint = nan;
% i = find(M(:,1)==1);
% boundaryx = [nan; nan];
% boundarydx = [nan; nan];
% boundarydxx = [nan; nan];
% if isnan(leftright)
%     intropart = i(1) : i(end);
%     iEndl = 1 : i(1) - 1;
%     iEndr = (i(end) + 1) : size(M,1);
%     iEndlvalid = find(~isnan(M(iEndl, 4)));
%     iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
%     ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
%     iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
% elseif leftright == -1
%     intropart = i(1) : size(M,1);
%     iEndl = 1 : (i(1) - 1);
%     iEndlvalid = find(~isnan(M(iEndl, 4)));
%     ivalid = i(1) + find(~isnan(M(intropart, 4))) - 1;
%     iinvalid = i(1) + find(isnan(M(intropart, 4))) - 1;
% else
%     intropart = 1: i(end);
%     iEndr = (i(end) + 1) : size(M,1);
%     iEndrvalid = i(end) + find(~isnan(M(iEndr, 4)));
%     ivalid = find(~isnan(M(intropart, 4)));
%     iinvalid = find(isnan(M(intropart, 4)));
% end
% xin = M(ivalid,2)';
% invalidx = M(iinvalid,2)';
% yin = M(ivalid,4)';
% w = M(ivalid,5)';
% xinub = M(ivalid,7)';%inf(1,n);
% xinub(xinub == 1) = inf;
% invalidupper = M(iinvalid, 7)';
% invalidupper(invalidupper == 1) = inf;
% invalidlower = M(iinvalid, 6);
% xinlb = M(ivalid,6)';%zeros(1,n);
% if isnan(leftright)
%     xexl = M(iEndlvalid, 2)';
%     yexl = M(iEndlvalid, 4)';
%     xendl = M(iEndl,2)';
%     xexr = M(iEndrvalid, 2)';
%     yexr = M(iEndrvalid, 4)';
%     xendr = M(iEndr,2)';
%     ubendl = M(iEndl, 7)';
%     ubendl(ubendl == 1) = inf;
%     ubendr = M(iEndr, 7)';
%     ubendr(ubendr == 1) = inf;
%     lbendl = M(iEndl, 6)';
%     lbendr = M(iEndr, 6)';
% elseif leftright == -1
%     xexl = M(iEndlvalid,2)';
%     xexr = [];
%     yexl = M(iEndlvalid,4)';
%     yexr = [];
%     xendl = M(iEndl,2)';
%     xendr = [];
%     ubendl = M(iEndl,7)';
%     ubendl(ubendl == 1) = inf;
%     ubendr = [];
%     lbendl = M(iEndl,6)';
%     lbendr = [];
% else
%     xexl = [];
%     xexr = M(iEndrvalid,2)';
%     yexl = [];
%     yexr = M(iEndrvalid,4)';
%     xendl = [];
%     xendr = M(iEndr,2)';
%     ubendl = [];
%     ubendr = M(iEndr,7)';
%     ubendr(ubendr == 1) = inf;
%     lbendl = [];
%     lbendr = M(iEndr,6)';
% end
% leftincrease = -inf;
% rightincrease = inf;

goodleft = false;
goodright = false;
joinedleft = false;
joinedright = false;
count = 0;
while ~goodleft || ~goodright
    %prefit
    if isnan(leftright) || leftright == 1
        %xl = xexl - xin(1);
        if isempty(xexl)
            boundarydxx(1) = nan;
            %boundarydx(1) = nan;
        else
            xl = xexl - xexl(1);
            if length(xl) < 3
                boundarydxx(1) = nan;
            elseif length(xl) == 4
                pl = csaps(xl, yexl);
                if (pl.coefs(end,3) < 0)
                    if all(diff(yexl)) <= 0
                        boundarydxx(1) = nan;
                    else
                        error('Invalid left polyfit');
                    end
                else
                    boundarydxx(1) = pl.coefs(end,2);
                end
            else
                pl = polyfit(xl, yexl, 3);
                if pl(3) > 0 % don't want to time fit strange left wing
                    if all(diff(yexl)) <= 0
                        boundarydxx(1) = nan;
                    else
                        error('Invalid left polyfit');
                    end
                else
                    boundarydxx(1) = pl(2);
                    %boundarydx(1) = pl(3);
                end
            end
        end
    end
    if boundarydxx(1) == 0
        boundarydxx(1) = nan;
    end
    if isnan(leftright) || leftright == -1
        if isempty(xexr)
            boundarydxx(2) = nan;
            %boundarydx(2) = nan;
        else
            xr = xexr - xin(end);
            if length(xr) < 3
                boundarydxx(2) = nan;
            elseif length(xr) == 4
                pr = csaps(xr, yexr);
                if (pr.coefs(1,3) < 0)
                    if all(diff(yexr)) >= 0
                        boundarydxx(2) = nan;
                    else
                        error('Invalid right polyfit');
                    end
                else
                    boundarydxx(2) = pr.coefs(1,2);
                end
            else
                pr = polyfit(xr, yexr, 3);
                if pr(3) < 0 % don't want to time fit strange right wing
                    if all(diff(yexr)) >= 0
                        boundarydxx(2) = nan;
                    else
                        error('Invalid right polyfit');
                    end
                else
                    boundarydxx(2) = pr(2);
                    %boundarydx(2) = pr(3);
                end
            end
        end
    end
    if boundarydxx(2) == 0
        boundarydxx(2) = nan;
    end
    
    %interpolation part
    % if isnan(leftright)
    %     [smoothCoeff1, exitflag, g, gamma, aaa, bbb, ccc, ddd, turningPoint,a, b] = flexWingFit(xin, yin, w, stationarypoint, [nan, nan], smoothCoeff, [nan, nan],  xleft, xright, dxleft, dxright, dxxleft, dxxright, nan, xinub, xinlb, [], [], [], [], [], [], invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange);
    % elseif leftright == -1 %left
    %     [smoothCoeff1, exitflag, g, gamma, aaa, bbb, ccc, ddd, turningPoint,a, b] = flexWingFit(xin, yin, w, stationarypoint, [nan, nan], smoothCoeff, [nan, nan], xleft, xright, dxleft, dxright, dxxleft, dxxright, nan, xinub, xinlb, xendl, ubendl, lbendl, [], [], [], invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange);
    % else %right
    %     [smoothCoeff1, exitflag, g, gamma, aaa, bbb, ccc, ddd, turningPoint,a, b] = flexWingFit(xin, yin, w, stationarypoint, [nan, nan], smoothCoeff, [nan, nan],  xleft, xright, dxleft, dxright, dxxleft, dxxright, nan, xinub, xinlb, [], [], [], xendr, ubendr, lbendr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange);
    % end
    
    try
        if isnan(leftright)
            [smoothCoeff1, exitflag, g, gamma, aaa, bbb, ccc, ddd, turningPoint, x] = flexWingFit(xin, yin, w, stationarypoint, tailConcavity, smoothCoeff, [nan, nan], boundaryx, boundarydx, boundarydxx, nan, xinub, xinlb, xendl, ubendl, lbendl, xendr, ubendr, lbendr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange, true);
        elseif leftright == -1 %left
            [smoothCoeff1, exitflag, g, gamma, aaa, bbb, ccc, ddd, turningPoint, x] = flexWingFit(xin, yin, w, stationarypoint, tailConcavity, smoothCoeff, [nan, nan], boundaryx, boundarydx, boundarydxx, nan, xinub, xinlb, xendl, ubendl, lbendl, [], [], [], invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange, true);
        else %right
            [smoothCoeff1, exitflag, g, gamma, aaa, bbb, ccc, ddd, turningPoint, x] = flexWingFit(xin, yin, w, stationarypoint, tailConcavity, smoothCoeff, [nan, nan], boundaryx, boundarydx, boundarydxx, nan, xinub, xinlb, [], [], [], xendr, ubendr, lbendr, invalidx, invalidupper, invalidlower, leftincrease, rightincrease, smooth, tight, tightlb, tightub, minxrange, true);
    end
    catch e
        if strcmp(e.message, 'right need more data')
            bbb(1) = inf;
            bbb(2) = 0;
        elseif strcmp(e.message, 'right need more data')
            bbb(1) = 0;
            bbb(2) = inf;
        else
            rethrow(e)
        end
    end
    
    leftflat = false;
    rightflat = false;
    if ~isempty(xexl) && (bbb(1) == 0) % should not be here       
        if ~isempty(xexl) && ~joinedleft
            idx = find(xendl, xexl(1));
            xin = [xexl xin];
            yin = [yexl yin];
            w = [ones(1, length(xexl)) * min(w) w];
            boundaryx = [nan nan];
            boundarydx = [nan nan];
            boundarydxx = [nan nan];
            xexl = [];
            yexl = [];
            xinlb = [lbendl(idx:end) xinlb];
            xinub = [ubendl(idx:end) xinub];
            xendl = xendl(1:idx-1);
            ubendl = ubendl(1:idx-1);
            lbendl = lbendl(1:idx-1);
            joinedleft = true;
            goodleft = false;
        else
            %error('Invalid left polyfit');
            goodleft = true;
        end
    else
        goodleft = true;
    end
    if ~isempty(xexr) && (bbb(2) == 0) % should not be here
        if ~isempty(xexr) && ~joinedright
            idx = length(xexr);
            xin = [xin xexr];
            yin = [yin yexr];
            w = [w ones(1, length(xexr)) * min(w)];
            boundaryx = [nan nan];
            boundarydx = [nan nan];
            boundarydxx = [nan nan];
            xexr = [];
            yexr = [];
            xinlb = [xinlb lbendr(1:idx)];
            xinub = [xinub ubendr(1:idx)];
            xendr = xendr(idx+1:end);
            ubendr = ubendr(idx+1:end);
            lbendr = lbendr(idx+1:end);
            joinedright = true;
            goodright = false;
        else
            %error('Invalid right polyfit');
            goodright = true;
        end
    else
        goodright = true;
    end
    
    count = count + 1;
    if (count > 3)
        err = 'Fail to time fit';
        error('%s', err);
    end
end


% leftincrease = max(leftincrease, minxrange);
% rightincrease = min(rightincrease, minxrange);

%exitflag
if (exitflag == 3 && leftright == -1) || exitflag ==4
    % if empty xexl, then we do have a flat left, else not real flat
    if ~isempty(xexl)
        exitflag = 1;
    end
elseif (exitflag == 3 && leftright == -1) || exitflag == 5
    if ~isempty(xexr)
        exitflag = 1;
    end
elseif exitflag == 6
    if ~isempty(xexr)&& ~isempty(xexl)
        exitflag = 1;
    elseif ~isempty(xexl)
        exitflag = 5;
    elseif ~isempty(xexr)
        exitflag = 4;
    end
end

aal = aaa(1);
bbl = bbb(1);
ccl = ccc(1);
ddl = ddd(1);
aar = aaa(2);
bbr = bbb(2);
ccr = ccc(2);
ddr = ddd(2);
% %left part
% if isnan(leftright) || leftright == 1
%     aal = a(1);
%     bbl = b(1);
% %     xl = xexl - xin(1);
% %     pl = polyfit(xl, yexl, 3);
% %     ddl = pl(1);
% %     ccl = pl(2);
% %     bbl = b(1);
% %     if (sign(bbl) ~= sign(pl(3)))
% %         error('Invalid left polyfit');
% %     end
% %     aal = a(1);% a(1) - bbl * xl(end) - ccl * xl(end)^2 - ddl * xl(end)^3;
% %     %     dxpoly = bbl + ccl * xl(end) + ddl * xl(end)^2;
% %     % %     % use the weighted last few b's to calculate extrapolating b
% %     % %     xtmp = xin(1: min(length(xin),3));
% %     % %     w = exp(-(xin(1)-xtmp)./(xin(1)-xtmp(end)) * 3);
% %     % %     btmp = b(1: min(length(b),3));
% %     % %     dxnew = w * btmp / sum(w);
% %     %     dxflexwingfit = b(1);
% %     %     ratio = dxflexwingfit / dxpoly;
% %     %     if ratio > 0
% %     %         bblratio = bbl * ratio;
% %     %         cclratio = ccl * ratio;
% %     %         ddlratio = ddl * ratio;
% %     %         difference = dxflexwingfit - dxpoly;
% %     %         bbl = bbl + difference;
% %     %
% %     %         %modify aa/bb/cc/dd such that they obey the boundary
% %     %         try
% %     %             [ccl] = splineHelper.parsToFitBoundaryc(xin(1), bblratio, cclratio, ddlratio, xendl, lbendl, ubendl, a(1), xl(end));
% %     %             if ccl / pl(2) < 0
% %     %                 error('Invalid left polyfit');
% %     %             end
% %     %             aal = a(1) - bbl * xl(end) - ccl * xl(end)^2 - ddl * xl(end)^3;
% %     %             bbl = bblratio;
% %     %             ddl = ddlratio;
% %     %             % left part should be monotone decreasing
% %     %             yleft = aal + bbl .* xl + ccl .* xl.^2 + ddl .* xl.^3;
% %     %             if ~all(diff(yleft) <= 0)
% %     %                 error('Invalid left polyfit');
% %     %             end
% %     %         catch
% %     try
% %         [bbl] = splineHelper.parsToFitBoundaryb(xin(1), aal, bbl, ccl, ddl, xendl, lbendl, ubendl);
% %         if bbl / pl(3) < 0
% % %         [ccl] = splineHelper.parsToFitBoundaryc(xin(1), aal, bbl, ccl, ddl, xendl, lbendl, ubendl);
% % %         if ccl / pl(2) < 0
% %             error('Invalid left polyfit');
% %         end
% %     catch % we don't want to modify b that makes it not smooth
% %         %                 [bbl] = splineHelper.parsToFitBoundaryb(xin(1), aal, bbl, ccl, ddl, xendl, lbendl, ubendl);
% %         %                 if bbl / pl(3) < 0
% %         error('Invalid left polyfit');
% %         %                end
% %     end
% %     %        end
% %     %     else
% %     %         error('Invalid left polyfit');
% %     %     end
% %
% %     % left part should be monotone decreasing(might be down and up, but that case we probabily want to use flexwing instead of time and the 'slope' should be
% %     % increasing
% %     yexll = [yexl g(1)];
% %     yleft = [aal + bbl .* xl + ccl .* xl.^2 + ddl .* xl.^3 g(1)];
% %     if ~all(diff(yleft) <= 0)
% %         error('Invalid left polyfit');
% %     end
% %     xleft = [xexl xin(1)];
% %     slopeleft = (diff(yleft)./diff(xleft));
% %     slopeleftinner = ((g(2) - g(1)) / (xin(2) - xin(1)));
% %     good = false;
% %     if sign(slopeleft(end)) == sign(slopeleftinner)
% %         slopeleft = abs(slopeleft(end));
% %         slopeleftinner = abs(slopeleftinner);
% %         if slopeleftinner / slopeleft(end) > 0.9 % connection point slope should not deecrease or even so not too much
% %             ratioleft = slopeleft(2:end)./slopeleft(1:end-1);
% %             if sum(ratioleft > 1) >= 0.5 * length(ratioleft) % should have 50% of the slope increasing in abs term
% %                 ratioleftend = ratioleft(1:end*0.1);
% %                 if isempty(ratioleftend) || all(ratioleftend) > 1.005 % very 10% end should have increasing slope
% %                     good = true;
% %                 end
% %             end
% %         end
% %     end
% %     if ~good
% %         error('Invalid left polyfit');
% %     end
% %
% %     if leftincrease > xendl(1)
% %         %make sure that left increase part is increase
% %         if leftincrease > xexl(end)
% %             xendtmp = xendl(end);
% %         else
% %             xendtmp = leftincrease;
% %         end
% %
% %         xregion = [xendl(1) xendtmp];
% %         s = splineHelper.size(xregion);
% %
% %         % if left end point < right end point, definitely wrong
% %         % it is possible to fix d to solve it but its over kill. this
% %         % situation probability means we have a wrong previous month's
% %         % shape
% %         if poly(aal, bbl, ccl, ddl, xregion(1) - xin(1)) < poly(aal, bbl, ccl, ddl, xregion(end) - xin(1))
% %             err = 'Fail to guarantee left increase';
% %             error('error in flexTimeFit: %s', err);
% %         end
% %
% %         % zero d makes second derivative linear so we will be monotone
% %         if ddl ~= 0
% %             %we check both critical points, if they are inside xendl, make
% %             %sure they are not local max/min (dxx = 0) or move the crital point
% %             %out of the region
% %             sq = sqrt(ccl*ccl - 3 * bbl * ddl);
% %             cp1 = (-ccl + sq)/3/ddl + xin(1);
% %             cp2 = (-ccl - sq)/3/ddl + xin(1);
% %
% %             %if critical point is inside the boundary
% %             if splineHelper.inRegion(cp1, xregion) || splineHelper.inRegion(cp2, xregion)
% %                 % if dd small, just flat it the make the critical point
% %                 % inflection point, notice that this changes makes us only have
% %                 % one inflection point so we don't need to consider both cp1
% %                 % and cp2
% %                 if (ddl <1e-5)
% %                     ddl = -ccl*ccl /3/ddl;
% %                 else
% %                     cp1v = poly(aal,bbl,ccl,ddl,cp1 - xin(1));
% %                     cp2v = poly(aal,bbl,ccl,ddl,cp2 - xin(1));
% %                     if cp1v < cp2v
% %                         minc = cp1;
% %                         minc1 = true;
% %                         maxc = cp2;
% %                     else
% %                         minc = cp2;
% %                         minc1 = false;
% %                         maxc = cp1;
% %                     end
% %                     % we should have critical point in sequence of max => min
% %                     if maxc > minc
% %                         err = 'Fail to guarantee left increase';
% %                         error('error in flexTimeFit: %s', err);
% %                     end
% %
% %                     good = false;
% %                     count = 0;
% %                     adjustedmin = false;
% %                     while ~good
% %                         if count == 2
% %                             % should not be here
% %                             err = 'Fail to guarantee left increase';
% %                             error('error in flexTimeFit: %s', err);
% %                         end
% %
% %                         %if min inside region
% %                         if ~good && splineHelper.inRegion(minc, xregion)
% %                             adjustedmin = true;
% %                             if xregion(end) - minc > 0.2 * s
% %                                 %if too far from the boundary
% %                                 if count == 1
% %                                     %if tried to adjust
% %                                     err = 'Fail to guarantee left increase';
% %                                     error('error in flexTimeFit: %s', err);
% %                                 end
% %                             else
% %                                 % if not too far, make it to the xendl(end)
% %                                 dtmp = -(2*ccl*xregion(2) + bbl)/3/xregion(2)/xregion(2);
% %
% %                                 if dtmp == 0
% %                                     good = true;
% %                                     ddl = dtmp;
% %                                     leftflat = true;
% %                                 else
% %                                     if minc1
% %                                         maxctmp = (-ccl - sq)/3/dtmp + xin(1);
% %                                         minctmp = (-cc1 + sq)/3/dtmp + xin(1);
% %                                     else
% %                                         maxctmp = (-ccl + sq)/3/dtmp + xin(1);
% %                                         minctmp = (-ccl - sq)/3/dtmp + xin(1);
% %                                     end
% %                                     maxcvtmp = poly(aal,bbl,ccl,ddl,maxctmp - xin(1));
% %                                     mincvtmp = poly(aal,bbl,ccl,ddl,minctmp - xin(1));
% %                                     % new maxc should be less than new minc and new maxcv should
% %                                     % be greter than new mincv
% %                                     if (maxctmp > minctmp || maxcvtmp < mincvtmp)
% %                                         err = 'Fail to guarantee left increase';
% %                                         error('error in flexTimeFit: %s', err);
% %                                     end
% %                                     %new minc should not be in the region
% %                                     if splineHelper.inRegion(minctmp, xregion)
% %                                         err = 'Fail to guarantee left increase';
% %                                         error('error in flexTimeFit: %s', err);
% %                                     end
% %                                     %if new maxctmp in region
% %                                     if splineHelper.inRegion(maxctmp, xregion)
% %                                         if count == 1
% %                                             % if already tried to fix max, we are in trouble
% %                                             err = 'Fail to guarantee left increase';
% %                                             error('error in flexTimeFit: %s', err);
% %                                         else
% %                                             % if first time here (didn't try to fix max)
% %                                             if maxctmp - xregion(1) > 0.2 * s && ...
% %                                                     ~splineHelper.inRegion(maxc, xregion)
% %                                                 % if new maxc is far from the boundary and
% %                                                 % old one is not in region, we cannot fix
% %                                                 % this problem, note that if the old one is
% %                                                 % in region we might be able to fix it
% %                                                 err = 'Fail to guarantee left increase';
% %                                                 error('error in flexTimeFit: %s', err);
% %                                             else
% %                                                 % we might be able to fix this problem by
% %                                                 % changing maxc
% %                                                 maxc = maxctmp;
% %                                             end
% %                                         end
% %                                     else
% %                                         %else we have new good minc and maxc
% %                                         good = true;
% %                                         ddl = dtmp;
% %                                     end
% %                                 end
% %                             end
% %                         end
% %
% %                         %if max inside region
% %                         if ~good && splineHelper.inRegion(maxc, xregion)
% %                             if maxc - xregion(1) > 0.2 * s
% %                                 %if too far from the boundary
% %                                 if adjustedmin || count == 1
% %                                     % cant fix if already tried to fix min
% %                                     err = 'Fail to guarantee left increase';
% %                                     error('error in flexTimeFit: %s', err);
% %                                 end
% %                             else
% %                                 % if not too far, make it to the xendl(1)
% %                                 dtmp = -(2*ccl*xregion(1) + bbl)/3/xregion(1)/xregion(1);
% %
% %                                 if dtmp == 0
% %                                     good = true;
% %                                     ddl = dtmp;
% %                                     leftflat = true;
% %                                 else
% %                                     if minc1
% %                                         maxctmp = (-ccl - sq)/3/dtmp + xin(1);
% %                                         minctmp = (-cc1 + sq)/3/dtmp + xin(1);
% %                                     else
% %                                         maxctmp = (-ccl + sq)/3/dtmp + xin(1);
% %                                         minctmp = (-ccl - sq)/3/dtmp + xin(1);
% %                                     end
% %                                     maxcvtmp = poly(aal,bbl,ccl,ddl,maxctmp - xin(1));
% %                                     mincvtmp = poly(aal,bbl,ccl,ddl,minctmp - xin(1));
% %                                     % new maxc should be greater than new minc and new maxcv
% %                                     % should be greater than new mincv
% %                                     if (maxctmp > minctmp || maxcvtmp < mincvtmp)
% %                                         err = 'Fail to guarantee left increase';
% %                                         error('error in flexTimeFit: %s', err);
% %                                     end
% %                                     %new maxc should not be in the region
% %                                     if splineHelper.inRegion(maxctmp, xregion)
% %                                         err = 'Fail to guarantee left increase';
% %                                         error('error in flexTimeFit: %s', err);
% %                                     end
% %                                     %if new minctmp in region
% %                                     if splineHelper.inRegion(minctmp, xregion)
% %                                         if adjustedmin || count == 1
% %                                             % if already tried to fix min, we are in trouble
% %                                             err = 'Fail to guarantee left increase';
% %                                             error('error in flexTimeFit: %s', err);
% %                                         else
% %                                             % if old min not in region(so no adjustedmin)
% %                                             if xregion(end) - minctmp < 0.2 * s
% %                                                 % if new min is too far from the boundary eror
% %                                                 err = 'Fail to guarantee left increase';
% %                                                 error('error in flexTimeFit: %s', err);
% %                                             else
% %                                                 % try to fix (new) min
% %                                                 minc = minctmp;
% %                                             end
% %                                         end
% %                                     else
% %                                         % here if both maxc and minc not in region
% %                                         good = true;
% %                                         ddl = dtmp;
% %                                     end
% %                                 end
% %                             end
% %                         end
% %
% %                         count = count + 1;
% %                     end
% %                     %here if left > right and no critical point inside, we are good
% %                 end
% %                 %after potential fix, check if good
% %                 if poly(aal, bbl, ccl, ddl, xregion(1) - xin(1)) < poly(aal, bbl, ccl, ddl, xregion(end) - xin(1))
% %                     err = 'Fail to guarantee left increase';
% %                     error('error in flexTimeFit: %s', err);
% %                 end
% %             end
% %         end
% %     end
% else
%     aal = aaa(1);
%     bbl = bbb(1);
%     ccl = ccc(1);
%     ddl = ddd(1);
% end

% %right part
% if isnan(leftright) || leftright == -1
%     xr = xexr - xin(end);
%     pr = polyfit(xr, yexr, 3);
%     ddr = pr(1);
%     ccr = pr(2);
%     bbr = b(end);
%     aar = a(end);% - bbr * xr(1) - ccr * xr(1)^2 - ddr * xr(1)^3;
%     %     dxpoly = bbr + ccr * xr(1) + ddr * xr(1)^2;
%     % %     xtmp = xin(length(xin) - min(length(xin),3) + 1 : end);
%     % %     w = exp(-(xtmp-xin(end))./(xtmp(1) - xin(end)) * 3);
%     % %     btmp = b(length(b) - min(length(b),3) + 1 : end);
%     % %     dxnew = w * btmp / sum(w);
%     %     dxflexwingfit = b(end);
%     %     ratio = dxflexwingfit / dxpoly;
%     %     if ratio > 0
%     %         bbrratio = bbr * ratio;
%     %         ccrratio = ccr * ratio;
%     %         ddrratio = ddr * ratio;
%     %         difference = dxflexwingfit - dxpoly;
%     %         bbr = bbr + difference;
%     %
%     %         %modify aa/bb/cc/dd such that they obey the boundary
%     %         try
%     %             [ccr] = splineHelper.parsToFitBoundaryc(xin(end),bbrratio, ccrratio, ddrratio, xendr, lbendr,ubendrm, a(end), xr(1));
%     %             if ccr / pr(2) < 0
%     %                 error('Invalid right polyfit');
%     %             end
%     %             aar = a(end) - bbr * xr(1) - ccr * xr(1)^2 - ddr * xr(1)^3;
%     %             bbr = bbrratio;
%     %             ddr = ddrratio;
%     %             % right part should be monotone decreasing
%     %             yright = aar + bbr .* xr + ccr .* xr.^2 + ddr .* xr.^3;
%     %             if ~all(diff(yright) >= 0)
%     %                 error('Invalid right polyfit');
%     %             end
%     %         catch
%     try
%         [bbr] = splineHelper.parsToFitBoundaryb(xin(end), aar, bbr, ccr, ddr, xendr, lbendr, ubendr);
%         if bbr / pr(3) < 0
% %         [ccr] = splineHelper.parsToFitBoundaryc(xin(end), aar, bbr, ccr, ddr, xendr, lbendr, ubendr);
% %         if ccr / pr(2) < 0
%             error('Invalid right polyfit');
%         end
%         %aar = a(end) - bbr * xr(1) - ccr * xr(1)^2 - ddr * xr(1)^3;
%     catch
%         %                 [bbr] = splineHelper.parsToFitBoundaryb(xin(end), aar, bbr, ccr, ddr, xendr, lbendr, ubendr);
%         %                 if bbr / pr(3) < 0
%         error('Invalid right polyfit');
%         % end
%     end
%     %         end
%     %     else
%     %         error('Invalid right polyfit');
%     %     end
%
%     % right part slope should be monotone decreasing
%     yright = [g(end) aar + bbr .* xr + ccr .* xr.^2 + ddr .* xr.^3];
%     if ~all(diff(yright) >= 0)
%         error('Invalid right polyfit');
%     end
%     xright = [xin(end) xexr];
%     sloperight = (diff(yright)./diff(xright));
%     sloperightinner = ((g(end) - g(end-1)) / (xin(end) - xin(end-1)));
%     good = false;
%     if  (sign(sloperight(1)) == sign(sloperightinner)) & (sloperight(1) / sloperightinner < 1.1) % connection point slope should not increase or even so not too much
%         ratioright = sloperight(2:end) ./ sloperight(1:end-1);
%         if sum(ratioright < 1) >= 0.5 * length(ratioright) % should have 50% of the slope decreasing
%             ratiorightend = ratioright(end*0.9:end);
%             if isempty(ratiorightend) || all(ratiorightend) < 1.005 % very 10 end should have decreasing slope
%                 good = true;
%             end
%         end
%     end
%     if ~good
%         error('Invalid right polyfit');
%     end
%
%     if rightincrease < xendr(end)
%         %make sure that right increase part is increase
%         if rightincrease < xendr(1)
%             xendtmp = xendr(1);
%         else
%             xendtmp = rightincrease;
%         end
%
%         xregion = [xendtmp xendr(end)];
%         s = splineHelper.size(xregion);
%
%         % if left end point < right end point, definitely wrong
%         % it is possible to fix d to solve it but its over kill. this
%         % situation probability means we have a wrong previous month's
%         % shape
%         if poly(aar, bbr, ccr, ddr, xregion(1) - xin(end)) > poly(aar, bbr, ccr, ddr, xregion(end) - xin(end))
%             err = 'Fail to guarantee right increase';
%             error('error in flexTimeFit: %s', err);
%         end
%
%         if ddr ~= 0
%             %we check both critical points, if they are inside xendl, make
%             %sure they are not local max/min (dxx = 0) or move the crital point
%             %out of the region
%             sq = sqrt(ccr * ccr - 3 * bbr * ddr);
%             cp1 = (-ccr + sq)/3/ddr + xin(end);
%             cp2 = (-ccr - sq)/3/ddr + xin(end);
%
%             %if critical point is inside the boundary
%             if splineHelper.inRegion(cp1, xregion) || splineHelper.inRegion(cp2, xregion)
%                 % if dd small, just flat it the make the critical point
%                 % inflection point, notice that this changes makes us only have
%                 % one inflection point so we don't need to consider both cp1
%                 % and cp2
%                 if (ddr <1e-5)
%                     ddr = -ccr*ccr /3/ddr;
%                 else
%                     % if dd not small we need to make sure both critical points
%                     % are outside of region
%                     cp1v = poly(aar,bbr,ccr,ddr,cp1 - xin(end));
%                     cp2v = poly(aar,bbr,ccr,ddr,cp2 - xin(end));
%                     if cp1v < cp2v
%                         minc = cp1;
%                         minc1 = true;
%                         maxc = cp2;
%                     else
%                         minc = cp2;
%                         minc1 = false;
%                         maxc = cp1;
%                     end
%                     % we should have critical point in sequence of min => max
%                     if maxc < minc
%                         err = 'Fail to guarantee right increase';
%                         error('error in flexTimeFit: %s', err);
%                     end
%
%                     good = false;
%                     count = 0;
%                     adjustedmin = false;
%                     while ~good
%                         if count == 2
%                             % should not be here
%                             err = 'Fail to guarantee right increase';
%                             error('error in flexTimeFit: %s', err);
%                         end
%
%                         %if min inside region
%                         if ~good && splineHelper.inRegion(minc, xregion)
%                             adjustedmin = true;
%                             %if too far from the boundary
%                             if minc - xregion(1) > 0.2 * s
%                                 if count == 1
%                                     % if already tried to adjust
%                                     err = 'Fail to guarantee right increase';
%                                     error('error in flexTimeFit: %s', err);
%                                 end
%                             else
%                                 % if not too far, make it to the xendl(end)
%                                 dtmp = -(2*ccr*xregion(2) + bbr)/3/xregion(end)/xregion(end);
%
%                                 if dtmp == 0
%                                     good = true;
%                                     ddr = dtmp;
%                                     rightflat = true;
%                                 else
%                                     if minc1
%                                         maxctmp = (-ccr - sq)/3/dtmp + xin(end);
%                                         minctmp = (-ccr + sq)/3/dtmp + xin(end);
%                                     else
%                                         maxctmp = (-ccr + sq)/3/dtmp + xin(end);
%                                         minctmp = (-ccr - sq)/3/dtmp + xin(end);
%                                     end
%                                     maxcvtmp = poly(aar,bbr,ccr,ddr,maxctmp - xin(end));
%                                     mincvtmp = poly(aar,bbr,ccr,ddr,minctmp - xin(end));
%                                     % new maxc should be greter than new minc and new maxcv should
%                                     % be greter than new mincv
%                                     if (maxctmp < minctmp || maxcvtmp < mincvtmp)
%                                         err = 'Fail to guarantee right increase';
%                                         error('error in flexTimeFit: %s', err);
%                                     end
%                                     %new minc should not be in the region
%                                     if splineHelper.inRegion(minctmp, xregion)
%                                         err = 'Fail to guarantee right increase';
%                                         error('error in flexTimeFit: %s', err);
%                                     end
%                                     %if new maxctmp in region
%                                     if splineHelper.inRegion(maxctmp, xregion)
%                                         if count == 1
%                                             % if already tried to fix max, we are in trouble
%                                             err = 'Fail to guarantee right increase';
%                                             error('error in flexTimeFit: %s', err);
%                                         else
%                                             % if first time here (didn't try to fix max)
%                                             if xregion(end) - maxctmp > 0.2 * s && ...
%                                                     ~splineHelper.inRegion(maxc, xregion)
%                                                 % if new maxc is far from the boundary and
%                                                 % old one is not in region, we cannot fix
%                                                 % this problem, note that if the old one is
%                                                 % in region we might be able to fix it
%                                                 err = 'Fail to guarantee left increase';
%                                                 error('error in flexTimeFit: %s', err);
%                                             else
%                                                 % we might be able to fix this problem by
%                                                 % changing maxc
%                                                 maxc = maxctmp;
%                                             end
%                                         end
%                                     else
%                                         %else we have new good minc and maxc
%                                         good = true;
%                                         ddr = dtmp;
%                                     end
%                                 end
%                             end
%                         end
%
%                         %if max inside region
%                         if ~good && splineHelper.inRegion(maxc, xregion)
%                             if  xregion(end) - maxc > 0.2 * s
%                                 %if too far from the boundary
%                                 if adjustedmin || count == 1
%                                     % if already tried to adjust
%                                     err = 'Fail to guarantee right increase';
%                                     error('error in flexTimeFit: %s', err);
%                                 end
%                             else
%                                 % if not too far, make it to the xendl(1)
%                                 dtmp = -(2*ccr*xregion(1) + bbr)/3/xregion(1)/xregion(1);
%
%                                 if dtmp == 0
%                                     good = true;
%                                     ddr = dtmp;
%                                     rightflat = true;
%                                 else
%                                     if minc1
%                                         maxctmp = (-ccr - sq)/3/dtmp + xin(end);
%                                         minctmp = (-ccr + sq)/3/dtmp + xin(end);
%                                     else
%                                         maxctmp = (-ccr + sq)/3/dtmp + xin(end);
%                                         minctmp = (-ccr - sq)/3/dtmp + xin(end);
%                                     end
%                                     maxcvtmp = poly(aar,bbr,ccr,ddr,maxctmp - xin(end));
%                                     mincvtmp = poly(aar,bbr,ccr,ddr,minctmp - xin(end));
%                                     % new maxc should be greater than new minc and new maxcv
%                                     % should be greater than new mincv
%                                     if (maxctmp < minctmp || maxcvtmp < mincvtmp)
%                                         err = 'Fail to guarantee right increase';
%                                         error('error in flexTimeFit: %s', err);
%                                     end
%                                     %new maxc should not be in the region
%                                     if splineHelper.inRegion(maxctmp, xregion)
%                                         err = 'Fail to guarantee right increase';
%                                         error('error in flexTimeFit: %s', err);
%                                     end
%                                     %if new minctmp in region
%                                     if splineHelper.inRegion(minctmp, xregion)
%                                         if adjustedmin || count == 1
%                                             % if already tried to fix min, we are in trouble
%                                             err = 'Fail to guarantee right increase';
%                                             error('error in flexTimeFit: %s', err);
%                                         else
%                                             % if old min not in region(so no adjustedmin)
%                                             if minctmp - xregion(1) < 0.2 * s
%                                                 % if new min is too far from the boundary eror
%                                                 err = 'Fail to guarantee right increase';
%                                                 error('error in flexTimeFit: %s', err);
%                                             else
%                                                 % try to fix (new) min
%                                                 minc = minctmp;
%                                             end
%                                         end
%                                     else
%                                         % here if both maxc and minc not in region
%                                         good = true;
%                                         ddr = dtmp;
%                                     end
%                                 end
%                             end
%                         end
%
%                         count = count + 1;
%                     end
%                     %here if left > right and no critical point inside, we are good
%                 end
%                 %after potential fix we need to check if good
%                 if poly(aar, bbr, ccr, ddr, xregion(1) - xin(end)) > poly(aar, bbr, ccr, ddr, xregion(end) - xin(end))
%                     err = 'Fail to guarantee right increase';
%                     error('error in flexTimeFit: %s', err);
%                 end
%             end
%         end
%     end
% else
%     aar = aaa(2);
%     bbr = bbb(2);
%     ccr = ccc(2);
%     ddr = ddd(2);
% end

aa = [aal, aar];
bb = [bbl, bbr];
cc = [ccl, ccr];
dd = [ddl, ddr];

if leftflat && rightflat
    exitflag = 6;
elseif leftflat
    if leftright == -1
        exitflag = 3;
    else
        exitflag = 4;
    end
elseif rightflat
    if leftright == 1
        exitflag = 3;
    else
        exitflag = 5;
    end
end

% %print
% xl = xendl - xin(1);
% if leftright == -1 || aal == -1
%     yyl = [];
% else
%     yyl = aal + bbl * xl + ccl * xl.^2 + ddl * xl.^3;
% end
% xr = xendr - xin(end);
% if leftright == 1 || aar == -1
%     yyr = [];
% else
%     yyr = aar + bbr * xr + ccr * xr.^2 + ddr * xr.^3;
% end
% ynew = [yyl g' yyr];
% if (isempty(yexl))
%     yendl = nan(1, length(xendl));
% else
%     yendl = padarray(yexl, [0 length(xendl)-length(yexl)], nan, 'pre');
% end
% if (isempty(yexr))
%     yendr = nan(1, length(xendr));
% else
%     yendr = padarray(yexr, [0 length(xendr)-length(yexr)], nan, 'post');
% end
% yold = [yendl yin yendr];
% lb = [lbendl xinlb lbendr];
% ub = [ubendl xinub ubendr];
% x = [xendl xin xendr];
% plot(x,yold,x,ynew);
% legend('origin','poly');
% disp('x, yold, ynew, lb, ub');
% [x' yold' ynew' lb' ub']
% disp('aa,bb,cc,dd');
% [aa' bb' cc' dd']
% x
end

function [v] = poly(aa, bb, cc, dd, x)
v = aa + bb * x + cc * x.^2 + dd * x.^3;
end