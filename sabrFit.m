function [exitflag, alpha, nu, rho] = sabrFit(kvalid,kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, alpha, beta, nu, rho, lowerboundvalid, upperboundvalid, lowerboundinvalid, upperboundinvalid)
%     clc
%     M = csvread('c:\temp\voltooltest\slice_SPY.USA_20141219_core.csv', 1);
%     i = find(M(:,1)' == 1);
%     iinvalid = find(M(:,1)' ~= 1);
%     atfvol = 0;
%     alpha = 0.25;
%     beta = 0.5;
%     nu = 0.3;
%     rho = 0.7;
%     kvalid = M(i,2)';
%     kinvalid = M(iinvalid, 2)';
%     y = M(i,8)';
%     wvalid = M(i,7)';
%     winvalid = M(iinvalid, 7)';
%     ttm = 2.011;
%     fwd = 190.8512;
%     upperBoundvalid = M(i,9)';%inf(1,n);
%     upperBoundvalid(upperBoundvalid == 1) = inf;
%     upperBoundinvalid = M(iinvalid,9)';%inf(1,n);
%     upperBoundinvalid(upperBoundinvalid == 1) = inf;
%     lowerBoundvalid = M(i,10)';%zeros(1,n);
%     lowerBoundinvalid = M(iinvalid, 10)';

displayOn = false;
if displayOn
    options = optimoptions(@lsqnonlin, 'MaxFunEvals', 500);
else
    options = optimoptions(@lsqnonlin, 'MaxFunEvals', 500, 'Display', 'off');
end

if (atfvol == 0)
    method = 1;
else
    method = 2;
end

if method == 1
    objFun = @(x) calerror1(x, beta, kvalid, kinvalid, y, wvalid, winvalid, fwd, ttm, lowerboundvalid, upperboundvalid, lowerboundinvalid, upperboundinvalid);
    [x,~,~,exitflag] = lsqnonlin(objFun, [alpha, nu, rho], [0 0 -1], [inf, inf, 1], options);
    
    alpha = x(1);
    nu = x(2);
    rho = x(3);
else
    objFun = @(x) calerror2(x, beta, kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, lowerboundvalid, upperboundvalid, lowerboundinvalid, upperboundinvalid);
    [x,~,~,exitflag] = lsqnonlin(objFun, [nu, rho], [0 -1], [inf, 1], options);
    
    nu = x(1);
    rho = x(2);
    alpha = findAlpha(atfvol, fwd, ttm, beta, nu, rho);
end

if displayOn
    disp('alpha nu rho');
    [alpha nu rho]
    
    % if method == 1
    gvalid = sabr(kvalid, fwd, ttm, alpha, beta, nu, rho);
    ginvalid = sabr(kinvalid, fwd, ttm, alpha, beta, nu, rho);
    % else
    %     gvalid = sabrimplied(kvalid, atfvol, fwd, ttm, beta, nu, rho);
    %     ginvalid = sabrimplied(kinvalid, atfvol, fwd, ttm, beta, nu, rho);
    % end
    disp('valid: k y g lower upper');
    [kvalid' y' gvalid' lowerboundvalid' upperboundvalid']
    disp('invalid: k g lower upper');
    [kinvalid' ginvalid' lowerboundinvalid' upperboundinvalid']
    
    plot(kvalid,y,kvalid,gvalid);
end
end


function [v] = sabr(k, fwd, ttm, alpha, beta, nu, rho)
oneMinusBeta = 1 - beta;
A = (fwd * k) .^ oneMinusBeta;
sqrtA = sqrt(A);

logM = (abs(fwd - k) >= 1e-2) .* log(fwd./k) + (abs(fwd - k) < 1e-2) .* logMHelper(fwd, k);
z = (nu / alpha) * sqrtA .* logM;
B = 1 - 2 * rho .* z + z.^2;
C = oneMinusBeta ^2 .* logM.^2;
tmp = (sqrt(B) + z - rho) / (1 - rho);
xx = log(tmp);
D = sqrtA .* (1 + C / 24 + C.^2 / 1920);
d = 1 + ttm * (oneMinusBeta ^2 * alpha^2 ./ (24 * A) + 0.25 * rho * beta * nu * alpha ./ sqrtA + (2 - 3 * rho ^2) * (nu ^2 / 24));

m = 2.2204460492503131e-016 * 10;
multiplier = (abs(z.^2) > m) .* (z./xx) + (abs(z.^2) <= m) .* (1 - 0.5 * rho * z - (3 * rho ^2 - 2) * z.^2 /12);

v = (alpha ./ D) .* multiplier .* d;

    function logM = logMHelper(fwd, k)
        eps = (fwd - k)  ./ k;
        logM = eps - 0.5 * eps.^2;
    end
end

function [v] = sabrimplied(k, atmvol, fwd, ttm, beta, nu, rho)
v = sabr(k, fwd, ttm, findAlpha(atmvol, fwd, ttm, beta, nu, rho), beta, nu, rho);
end

function [atmVol2SabrAlpha] = findAlpha(atmvol, fwd, ttm, beta, nu, rho)
alpharoots = @(n, r) roots([...
    (1 - beta)^2*ttm/24/fwd^(2 - 2*beta) ...
    r*beta*n*ttm/4/fwd^(1 - beta) ...
    (1 + (2 - 3*r^2)*n^2*ttm/24) ...
    -atmvol*fwd^(1 - beta)]);

atmVol2SabrAlpha = min(real(arrayfun(@(x) ...
    x*(x>0) + realmax*(x<0 || abs(imag(x))>1e-6), alpharoots(nu, rho))));
end

function [error] = calerror1(x, beta, kvalid, kinvalid, y, wvalid, winvalid, fwd, ttm, lowerBoundvalid, upperBoundvalid, lowerBoundinvalid, upperBoundinvalid)
yyvalid = sabr(kvalid, fwd, ttm, x(1), beta, x(2), x(3));
wrongidxvalid = (yyvalid > upperBoundvalid) + (yyvalid < lowerBoundvalid);
errorvalid = (wrongidxvalid) .* 100 .* sqrt(wvalid) .* (y - yyvalid) + (ones(1,length(y)) - wrongidxvalid) .* sqrt(wvalid) .* (y - yyvalid);

yyinvalid = sabr(kinvalid, fwd, ttm, x(1), beta, x(2), x(3));
sqrtwinvalid = sqrt(winvalid);
wrongidxinvalidgreater = yyinvalid > upperBoundinvalid;
wrongidxinvalidlesser = yyinvalid < lowerBoundinvalid;
upper = (yyinvalid - upperBoundinvalid);
upper(isinf(upper)) = 0;
lower = (lowerBoundinvalid - yyinvalid);
lower(isinf(lower)) = 0;
errorinvalid = (wrongidxinvalidgreater) .* 100 .* sqrtwinvalid .* upper + (wrongidxinvalidlesser) .* 100 .* sqrtwinvalid .* lower;

error = [errorvalid errorinvalid];
end

function [error] = calerror2(x, beta, kvalid, kinvalid, y, wvalid, winvalid, fwd, atfvol, ttm, lowerBoundvalid, upperBoundvalid, lowerBoundinvalid, upperBoundinvalid)
yyvalid = sabrimplied(kvalid, atfvol, fwd, ttm, beta, x(1), x(2));
wrongidxvalid = (yyvalid > upperBoundvalid) + (yyvalid < lowerBoundvalid);
errorvalid = (wrongidxvalid) .* 100 .* sqrt(wvalid) .* (y - yyvalid) + (ones(1,length(y)) - wrongidxvalid) .* sqrt(wvalid) .* (y - yyvalid);

yyinvalid = sabrimplied(kinvalid, atfvol, fwd, ttm, beta, x(1), x(1));
sqrtwinvalid = sqrt(winvalid);
wrongidxinvalidgreater = yyinvalid > upperBoundinvalid;
wrongidxinvalidlesser = yyinvalid < lowerBoundinvalid;
upper = (yyinvalid - upperBoundinvalid);
upper(isinf(upper)) = 0;
lower = (lowerBoundinvalid - yyinvalid);
lower(isinf(lower)) = 0;
errorinvalid = (wrongidxinvalidgreater) .* 100 .* sqrtwinvalid .* upper + (wrongidxinvalidlesser) .* 100 .* sqrtwinvalid .* lower;

error = [errorvalid errorinvalid];
end



