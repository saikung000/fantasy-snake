# Technical decision document

Fantasy Snake is a project for Nanuq's Programmer testing. A Snake Game with RPG element in it. This document will explain the details and process of the technical decisions during made this game.

## 1. Architecture Pattern 
On start the project,i have to choose which pattern will be use in project. there are many architecture pattern  to use in game. base on my experience, i have 3 choose.
 - **MVC(Model-View-Controller)**  : easy to create fast to make game.
 - **MVP(Model-View-Presenter**)  : more complex but make code clear
 - **MVVM (Model-View-ViewModel)** : most complex. use data driven. but update between data and view automatic
#### Conclusion
I choose  **MVP(Model-View-Presenter)** because : 
1. I familiar with it , that help me can make game in time.  
2. It separates ui , model and input from business logic.
3. Easy to reuse or swaping component.
4. Good in update between data and view that this game might be.
5. Cannot use plugin create with MVVM will be harder
by that i want to use *Observer Pattern** for helping connect between data and view but cannot use plugin so i  use **Event** and **Action** instead for easy connect data and view.

## 2. 3D or 2D
In requirements not specify to be 3Dor 2D and i will be effect with coding and asset to use in game 
#### Conclusion
I choose to make **3D** because it can switch to 2D easier by change camera. and i can find asset that can use in 3D easier than 2D. 3d can show animation and looking great in game. 

## 3. Spawning object
Object in map must spawning in empty space not overlap with other object. i have to find way and optimize to  find  position spawn new object. I have 2 idea at first.
1.  Create grid data that save position has space. can calculate check data in grid to find position.
2. Loop random position and physic overlap to check position is empty
#### Conclusion
 I use **loop random and physics overlap** to check spawn position have space enough to spawn object. but i make limit loop to make sure game will not crash if it cannot find position. this is easier way and good to deal with map have change postion object like player hero. 
But if more object to spawn and may has less empty space for spawn. it must be change logic more optimize and take more time.
