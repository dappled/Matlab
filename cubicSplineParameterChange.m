clc
a = 0.2277;
b = -0.0129;
c = -5.1690e-04;
d = -6.8920e-06;

t = 120;

f = @(x) a + b * (x-t) + c*(x-t)^2 + d*(x-t)^3;
fplot(f, [80 120], 'b');
hold

b = b * 2;
f = @(x) a + b * (x-t) + c*(x-t)^2 + d*(x-t)^3;
fplot(f, [80 120], 'r');

b = b /2;
c = c * 2;
f = @(x) a + b * (x-t) + c*(x-t)^2 + d*(x-t)^3;
fplot(f, [80 120], 'g');

c = c /2 ;
d = d*2;
f = @(x) a + b * (x-t) + c*(x-t)^2 + d*(x-t)^3;
fplot(f, [80 120], 'k');

hold
