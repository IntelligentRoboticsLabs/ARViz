# ARViz

Intelligent Robotics http://intelligentrobotics.es/ @IntellRobotLabs 

ARViz is an RViz implementation for Augmented Reality. This application runs inside the Microsoft Hololens AR device as a UWP application, without the need to use Ros Bridge. This project has as goal to combine:


* TF2 support for ROS C#.
* Secure communications.
* Hololens Positioning in the scene.

This project uses the [UWP port of ROS2](https://github.com/esteve/ros2_dotnet) done by [Esteve Fernández](https://github.com/esteve). You can have a look at the talk he gave at ROSCon 2018 about ROS2 for Android, iOS and UWP to understand the underlying code [Video](https://vimeo.com/293302046) [Slides](https://roscon.ros.org/2018/presentations/ROSCon2018_ROS2%20for%20Android,%20iOS%20and%20Universal%20Windows%20Platform.pdf).

This project is funded by [ROSIN](http://rosin-project.eu/) as a focused Technical Project.

Project mantainers:
* David Vargas (dvargas@inrobots.es)
* Francisco Martín (fmrico@gmail.com)

Advisor:
* Esteve Fernández (esteve@apache.org)


***
<!-- 
    ROSIN acknowledgement from the ROSIN press kit
    @ https://github.com/rosin-project/press_kit
-->

<a href="http://rosin-project.eu">
  <img src="http://rosin-project.eu/wp-content/uploads/rosin_ack_logo_wide.png" 
       alt="rosin_logo" height="60" >
</a>

Supported by ROSIN - ROS-Industrial Quality-Assured Robot Software Components.  
More information: <a href="http://rosin-project.eu">rosin-project.eu</a>

<img src="http://rosin-project.eu/wp-content/uploads/rosin_eu_flag.jpg" 
     alt="eu_flag" height="45" align="left" >  

This project has received funding from the European Union’s Horizon 2020  
research and innovation programme under grant agreement no. 732287. 

## Building and using ROS2 for UWP

Follow the instructions on [ros2-dotnet](https://github.com/esteve/ros2_dotnet) to compile [ROS2 for UWP](https://github.com/esteve/ros2_dotnet/blob/master/README.md#universal-windows-platform-arm-win32-win64)
https://github.com/esteve/ros2_dotnet  

## Development Status

|                  | Task                                                                                                                      | Status                                                                                                                                  | Progress                    |
|---------------|---------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|-----------------------------|
| **Milestone 1** |                                                                                                                           |                                                                                                                                         |                             |
|               | Hololens positioning. Hololens node which publish TF2 messages with the  device position with respect the world  frame.   | Finished development of TF2 bindings for rcldotnet at https://github.com/IntelligentRoboticsLabs/geometry2.git  (branch:rcldotnet)               | 100 %  :white_check_mark:                    |
|               | User AR Interface                                                                                                         | Finished initial version                                                                                                                        | 100 %  :white_check_mark:                    |
|               | Final Development of C# bindings                                                                                          | Finished development of Nested types and Collections, needed for TF2 bindings, finished. PRs pending in  https://github.com/esteve/ros2_dotnet   | 100 %  :white_check_mark: |
|               | AR representation of basic types: scalar,  images, pointcloud, laser, TFs, Maps, PoseArray  and Paths                     | **Work in progress**.   PointCloud and PoseArray pending designs.                                                                           | 80 % :x:                    |
| **Milestone 2** |                                                                                                                           |                                                                                                                                         |                             |
|               | Integration of data types in rcldotnet                                                                                    | **Work in progress**                                                                                                                        | 90 % :x:                    |
|               | Acceptance tests and final design of user AR Interface                                                                   | **Work in progress**.  Last details of AR UI in progress. Acceptance tests pending the finalization of the final UI design.                    | 50 % :x:                     |
|               | Multi robot in same scene                                                                                                | **Work in progress**.  Multiple robots identified in same scene using ArUCOs. Working on the correct user interaction                        | 50 % :x:                     |
| **Milestone 3** |                                                                                                                           |                                                                                                                                         |                             |
|               | Unitary and integrations tests                                                                                            |                                                                                                                                         | 0 % :x:                     |
|               | System final acceptance questionnaire                                                                                     |                                                                                                                                         |                             |
|               | Final Documentation                                                                                                       | Works in  https://github.com/esteve/ros2_dotnet                                                                                         | 20 % :x:                    |


## Videos & demos

### Global positioning system

[![ARViz Hololens Positioning](http://img.youtube.com/vi/lQXtoK3w5X8/0.jpg)](https://www.youtube.com/watch?v=lQXtoK3w5X8 "ARViz Hololens Positioning")

[![ARViz GUI and Data Visualization](http://img.youtube.com/vi/mGTKNB-Iog0/0.jpg)](https://www.youtube.com/watch?v=mGTKNB-Iog0 "ARViz GUI and Data Visualization")

