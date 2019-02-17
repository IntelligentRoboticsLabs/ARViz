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

## Building and using ROS2 for UWP

Follow the instructions on [ros2-dotnet](https://github.com/esteve/ros2_dotnet) to compile [ROS2 for UWP](https://github.com/esteve/ros2_dotnet/blob/master/README.md#universal-windows-platform-arm-win32-win64)
https://github.com/esteve/ros2_dotnet  

## Development Status

|               | Task                                                                                                                      | Status                                                                                                                                  | Progress                    |
|---------------|---------------------------------------------------------------------------------------------------------------------------|-----------------------------------------------------------------------------------------------------------------------------------------|-----------------------------|
| **Milestone 1** |                                                                                                                           |                                                                                                                                         |                             |
|               | Hololens positioning. Hololens node which publish TF2 messages with the  device position with respect the world  frame.   | Development of TF2 bindings for rcldotnet at https://github.com/IntelligentRoboticsLabs/geometry2.git  (branch:rcldotnet)               | 90 % :x:                    |
|               | User AR Interface                                                                                                         | Work in progress                                                                                                                        | 50 % :x:                    |
|               | Final Development of C# bindings                                                                                          | Development of Nested types and Collections, needed for TF2 bindings, finished. PRs pending in  https://github.com/esteve/ros2_dotnet   | 100 %  :white_check_mark: |
|               | AR representation of basic types: scalar,  images, pointcloud, laser, TFs, Maps, PoseArray  and Paths                     | Work in progress                                                                                                                        | 50 % :x:                    |
| **Milestone 2** |                                                                                                                           |                                                                                                                                         |                             |
|               | Integration of data types in rcldotnet                                                                                    | Work in progress                                                                                                                        | 80 % :x:                    |
|               | Aceptation tests and final design of  user AR Interface                                                                   |                                                                                                                                         | 0 % :x:                     |
|               | Multi robot in same scene                                                                                                 |                                                                                                                                         | 0 % :x:                     |
| **Milestone 3** |                                                                                                                           |                                                                                                                                         |                             |
|               | Unitary and integrations tests                                                                                            |                                                                                                                                         | 0 % :x:                     |
|               | System final acceptance questionnaire                                                                                     |                                                                                                                                         |                             |
|               | Final Documentation                                                                                                       | Works in  https://github.com/esteve/ros2_dotnet                                                                                         | 20 % :x:                    |
