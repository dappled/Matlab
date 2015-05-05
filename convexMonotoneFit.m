function [yy] = convexMonotoneFit(x, y)
%clc
%M = csvread('c:\temp\voltooltest\sampledata.csv', 2, 0);
%MM = M(M(:,5)>=0.6, :);
%IV = MM(MM(:,3)~=0 & MM(:,4)~=0,:);
%IV = [IV (IV(:,3)+IV(:,4))./2];
%x = IV(:,2)';
%y = IV(:,6)';

h = x(2:end) - x(1:end-1);

n = length(x);
%originSecD = ((y(1:n-2) - y(2:n-1))./h(1:end-1) - (y(2:n-1) - y(3:n))./h(2:end))./((h(1:end-1) + h(2:end))/2)
% slm = slmengine(x,y,'plot','on', 'ConcaveUp', 'On', 'Decreasing', 'On');
% plotslm(slm);

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

%K = Q/R*Q';
%g = inv(eye(n) + 2 * K) * y

%g = y;
%gamma = zeros(1, n -2)';
eta = [y zeros(1, n-2)]';
%gg = [g', gamma']';
A = [Q; -R'];
D = blkdiag(eye(n,n), 2*R);

Aeq = A';
Beq = zeros(n-2, 1);
lb = zeros(2*n-2, 1);
Ales = [1/h(1), -1/h(1), zeros(1, n-2), 0, h(1)/6, zeros(1, n-4)];%; zeros(1,n-2), 1/h(n-1), -1/h(n-1), zeros(1, n-3), -h(n-1)/6];
bles = zeros(1,1);
yy = quadprog(D, -eta, -Ales, bles, Aeq, Beq, lb);
%plot(x, re(1:n))
%yy = re(1:n);
%secD = ((yy(1:n-2) - yy(2:n-1))./h(1:end-1) - (yy(2:n-1) - yy(3:n))./h(2:end))./((h(1:end-1) + h(2:end))/2)