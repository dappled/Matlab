function [traceA] = calculateTraceA(Q, R, smoothCoeff)
B = R + smoothCoeff * (Q' * Q);
[l, D] = ldl(B);
n = length(B);
b = zeros(n,n);
d = diag(D);
% calculate inverse b
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

% for i = n-2:-1:1
%     b(i,i+2) = -l(i+1,i)*b(i+2,i+2);
%     b(i,i+1) = -l(i+1,i)*b(i+1,i+1) - l(i+2,i+1)*b(i+1,i+2);
%     b(i,i) = 1/d(i) - l(i+1, i)*b(i,i+1) - l(i+2,i)*b(i,i+2);
% end

tmp = zeros(1,n+2);
%tmp(1) = Q(1, 1) ^ 2 * b(1,1) + Q(1,2) ^ 2 * b(2,2) + 2 * Q(1,1) * Q(1,2) * b(1,2);
tmp(1) = Q(1,1) * (Q(1, 1) * b(1, 1));
tmp(2) = Q(2,1) * (Q(2, 1) * b(1, 1) + Q(2, 2) * b(1, 2)) ...
       + Q(2,2) * (Q(2, 1) * b(2, 1) + Q(2, 2) * b(2, 2));
for i = 3:n
    %tmp(i) = Q(i,i-1) ^2 * b(i-1,i-1) + Q(i,i) ^ 2 * b(i,i) + Q(i,i+1) ^ 2 * b(i+1, i+1) + 2 * Q(i, i-1) * Q(i,i) * b(i-1,1) + 2 * Q(i,i-1) * Q(i, i+1) * b(i-1, i+1) + 2 * Q(i,i) * Q(i, i+1) * b(i, i+1);
    tmp(i) = Q(i,i-2) * (Q(i, i-2) * b(i-2, i-2) + Q(i, i-1) * b(i-2, i-1) + Q(i, i) * b(i-2, i)) ...
           + Q(i,i-1) * (Q(i, i-2) * b(i-1, i-2) + Q(i, i-1) * b(i-1, i-1) + Q(i, i) * b(i-1, i)) ...
           + Q(i,i  ) * (Q(i, i-2) * b(i  , i-2) + Q(i, i-1) * b(i  , i-1) + Q(i, i) * b(i, i));
end
tmp(n+1) = Q(n+1,n-1) * (Q(n+1, n-1) * b(n-1, n-1) + Q(n+1, n) * b(n-1, n)) ...
         + Q(n+1,n )  * (Q(n+1, n-1) * b(n  , n-1) + Q(n+1, n) * b(n  , n));
%tmp(n) = Q(n, n-1) ^ 2 * b(n-1, n-1) + Q(n,n) ^ 2 * b(n, n) + 2 * Q(n, n-1) * Q(n,n) * b(n-1, n); 
tmp(n+2) = Q(n+2,n  ) * (Q(n+2, n  ) * b(n, n));

traceA = size(Q,1) - smoothCoeff * sum(tmp);
end