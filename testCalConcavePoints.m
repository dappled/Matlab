function testCalConcavePoints()
clc
leftRight = -1;
x = [140 150 160 170 180]
originSecD = [0.2 0.3 0.4]
n = 5;
turningPoint = NaN;
[concavePoints,turningPoint] = calConcavePoints(x, n, originSecD, turningPoint, leftRight)

    function [concavePoints,turningPoint] = calConcavePoints(x, n, originSecD, turningPoint, leftRight)
        if leftRight == -1
            if isnan(turningPoint)
                idx = find(cumsum(originSecD <= 0) >= ((1:1:n-2) * 2/3));
                if isempty(idx)
                    concavePoints = 0;
                    turningPoint = x(1);
                else
                    idxx = find(originSecD(1:idx(end)) <= 0);
                    concavePoints = idxx(end);
                    turningPoint = x(concavePoints + 1);
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
end