Feature: Can win the Memba Game
    Background:
        Given I am on the Memba game page
        
        @PlayWinGame
        Scenario: Ensure the Memba game win condition is working as expected
            When I click each unique memba
            Then I should see a score of 12
        
        @PlayShortGame
        Scenario: To see a single click increment the score
            When I click the 1 memba
            Then I should see a score of 1
        