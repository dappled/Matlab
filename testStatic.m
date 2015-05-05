classdef testStatic
    methods(Static)
                function wonAmount = getPayout(gambledAmount, noGuesses)
            wonAmount = 'something';
            disp('a');
        end
        function [wonAmount, noGuesses] = run(gambledAmount)
            noGuesses = 'something';
            wonAmount = highLowGame.getPayout(gambledAmount, noGuesses); % <---
        end

    end
end