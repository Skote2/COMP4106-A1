# COMP4106-A1: Spider game

Carleton University\
Winter 2019

Artificial Intelligence\
John Oommen\
Assignment 1

By: David N. Zilio (Skote2) \
Demo: 16:30 Wednesday Feb 27<sup>th</sup> 2019\
TA: Karim Hersi

This project is a simple snake like game where a spider navigates a grid to chase ants.
The emphasis for this project was that the spider is controlled by AI and demonstrates advantages and disadvantages of different mechanisms for problem solving. The AI was to use Breadth First Search (BFS), Depth First Search (DFS), and an A* search which implements two of my chosen heuristics.

## The Spider Game

More specifically our spider has a finite 9 moves it can make in its turn: "down=1, left=2, right=3, up2right=4, up2left=5, upRight2=6, upLeft2=7, downRight=8, downLeft=9" as were defined in the Board class object as an enumeration. When the spider makes its move the ant simultaneously acts by moving at its given speed in whichever cardinal direction it was spawned to face. The spider's objective is to move in such a way that it occupies the same square as the ant on a grid of known size by doing so it has won. Additional rules specify that the Spider loses if it leaves the grid and that a new ant is spawned once eaten or if it has left the grid.

### The System as a State Space

The appropriate mechanisms for solving this system are based on the rigid definition of the system itself. The system is defined such that there are a finite number of states, more specifically the number of possible states is the grid area choose two for the ant and spider (in the extra cases just add the number of extra ants or spiders). This defines a finite system which may realistically be solved and stored; of course, there are cases where a solution is impossible.

## Features & Design

Given that a large grid would be very expensive to solve for all states, given the exponential nature of grid growth, calculating a library of solutions is less than optimal. Similarly, some algorithms which pursue the former path will be inevitably inferior to others.

### Solutions

#### Searching

In some systems knowledge about the objective isn't always available, only that it exists and it's solvable. In either case where the agent trying to solve the problem knows some greater content or not, searching can be used to solve the system. In this case specifically the grid can be though of as a graph and the actions the spider makes connections between nodes. By doing so one refines this problem into something solved on a theoretical level many times before.

##### Breadth First Search (BFS)

By taking the system as a graph and the spider as the root node of this graph with "no knowledge" of the grid around it BFS is a relatively effective, but costly solution to the system. By queueing the spider's actions from its current node and iterating through the possibilities then continuing to loop through the queued options at each 'new' node eventually, wherever it is, the ant will be found. This grows radially and doesn't have to account for the spider going off the grid to find a solution. Once the ant is found by stacking the moves made to the ant on your way back to the root an efficient route is found to solve the problem. But doing so was taxing and if the nodes already navigated weren't stored the action set grows at an uncontrollable rate, not that the rate it grows and what it searches isn't already excessively taxing. BFS works, it always will it'll just search a lot of what doesn't matter on its way to the objective until its radial growth happens upon the solution.

##### Depth First Search (DFS)

Probably want to skim this.\
Depth first has an approach I'd like to think of as potentially better than breadth first but usually worse. It recursively goes down paths and when it bottoms out tries the next option from the deepest point continuing to bottom out and climb back up until it's either found a solution or explored everything. How you implement depth first and choose to exit it really changes its performance. If depth first has no idea where to go it's going to fully explore areas which could be opposite to the objective. There's also the disadvantage of the solution found is almost never the most direct route. Even by tracking the previously discovered nodes and updating the path if a better route is found depth first will still spend a lot of time doing things in the complete wrong direction just to come up with a poor solution if it finds one. Dept first if it isn't told to terminate on the wall of the grid may never find a solution to any problem, unlike BFS which will always find it, given enough time.\
But the slightest tweak to DFS can make it powerful. By having the vaguest idea of the direction to go DFS becomes very efficient, it can if told to kill negatively contributing trees and pick ones based off a momentum find a route to a far object far more efficiently than many others. Thing is doing stuff like this basically defines a new search mechanism.\
An interesting feature of DFS is that if allowed to run for longer it can find more efficient routes than its original solution. Since DFS will tend to walk around the outside of defined borders it can find very long solutions and; if routes to nodes are replaced by better solutions when rediscovered after enough time DFS does discover every route to every node and redefine over all the paths to create the best solutions

Regardless the thing is super inefficient because it just recurses off in whatever direction it wants and doesn't even come up with efficient solutions unless allowed to overwrite all of the work its done as it expels exponential amount of time searching over things its already done. But it's easy to implement.

##### Other searching algorithms

Searching can be far more efficient than either of the two above options. Namely Dijkstra’s search algorithm for searching could be implemented to find the best solution in the best time one only needs to think of the grid space as a graph. Additionally, any amount of controls and tweaks could make the algorithms above far more capable than they are, but tweaks usually require additional knowledge and must be applied conditionally, Dijkstra’s requires additional knowledge and without any knowledge BFS is all that can be done for an "efficient" search into nowhere.

#### Knowledge Systems

Routing algorithms are all built on searching with knowledge and that's all that this is. Defining an efficient route using some set of knowledge varies greatly but tends to revolve around mechanisms for calculating the distance between the searching agent and the objective. Pythagorean distance is commonly understood and applied due to its simplicity and Euclidian distance is used because of its generally better function but tends to be slightly more complex. Any implemented A* algorithm would be utilizing these general principles in an attempt to find more efficient routes in more efficient amounts of time. These metrics don't always help in systems which may have blocks or complex weightings to the graph raw distances tend not to be enough.

### Actual Implementation

I Built the game to have a functional BFS, occasionally functional DFS but no A*. additional requirements for everything but the multiple spiders have been implemented. Multiple spiders doesn’t work just because the Tree class which contains the AI had yet to become an attribute of the spiders and directly control their move actions; the rest of the infrastructure is there for the game to support multiple spiders.

* My searches were built to create a tree of nodes each with fake boards based on the moves made to get to them, the root being the current game board. The game boards were made to not be heavy by avoiding using an actual grid rather it synthesized it and based everything on raw co-ordinates held by stored Creature objects.
* A dictionary was kept with a history of the nodes and used to quickly determine what the best routes to a state was in the case that the child of a node found a pre-existing state. This functioned based on hashing functions I wrote to hash the game state such that different boards would be the same when compared rather than being compared as objects.
* DFS didn't work because the history kept game states rather than nodes which made it useless for discovering anything other than if a node had been visited already and caused DFS to break. I could fix it just by adding a hash for nodes and changing the key type of the history dictionary to a node.
* A* was not implemented but would've relied on treating the grid as a graph and implementing Dijkstra’s to best navigate the board.\
I also would've liked to use heuristics to create an "optimized" DFS which gave nodes scores based on their Euclidian & Pythagorean distance to the Ant and killed off trees which had the score decrease by a set acceptable amount. This would make the tweaked DFS evaluate a line to the Ant in a thickness of whatever my score tolerance is