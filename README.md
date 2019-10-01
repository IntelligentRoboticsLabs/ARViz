# ARViz

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

## Development Status

|  | Task | Status | Progress |
|--|--|--|--|
| **Milestone_1** |  |  |  |
| | Hololens positioning. Hololens node which publish TF2 messages with the  device position with respect the world  frame. | Finished development of TF2 bindings for rcldotnet at [IntelligentRoboticsLabs geometry2 repo](https://github.com/IntelligentRoboticsLabs/geometry2/tree/rcldotnet) (branch: rcldotnet) | 100%:white_check_mark:|
| | User AR Interface | Finished initial version | 100%:white_check_mark:|
| | Final Development of C# bindings | Finished development of Nested types and Collections, needed for TF2 bindings, finished. PRs pending in  https://github.com/esteve/ros2_dotnet | 100%:white_check_mark: |
| | AR representation of basic types: scalar, images, pointcloud, laser, TFs, Maps, PoseArray and Paths | | 100%:white_check_mark:|
| **Milestone_2** | | | |
| | Integration of data types in rcldotnet | | 100%:white_check_mark: |
| | Acceptance tests and final design of user AR Interface | **Work in progress.** Acceptance tests pending. | 90%:arrows_counterclockwise:|
| | Multi robot in same scene | Multiple robots identified in same scene with minimal initial setup required. | 100%:white_check_mark: |
| **Milestone_3** | | | |
| | Unitary and integrations tests | **Work in progress.** | 10%:arrows_counterclockwise: |
| | System final acceptance questionnaire | **Work in progress.** | 10%:arrows_counterclockwise: |
| | Final Documentation | **Work in progress.** [Wiki](https://github.com/IntelligentRoboticsLabs/ARViz/wiki)  | 80%:arrows_counterclockwise: |


## Videos & demos (the images link to the youtube video)

### Global positioning system
<!--
[![ARViz Hololens Positioning](http://img.youtube.com/vi/lQXtoK3w5X8/0.jpg)](https://www.youtube.com/watch?v=lQXtoK3w5X8 "ARViz Hololens Positioning")
-->
<p align="center">
    <a href="https://www.youtube.com/watch?v=lQXtoK3w5X8">
        <img src="http://img.youtube.com/vi/lQXtoK3w5X8/0.jpg" alt="ARViz Hololens Positioning">
    </a>
</p>

### ARViz GUI and Data Visualization
<!--
[![ARViz GUI and Data Visualization](http://img.youtube.com/vi/mGTKNB-Iog0/0.jpg)](https://www.youtube.com/watch?v=mGTKNB-Iog0 "ARViz GUI and Data Visualization")
-->
<p align="center">
    <a href="https://www.youtube.com/watch?v=mGTKNB-Iog0">
        <img src="http://img.youtube.com/vi/mGTKNB-Iog0/0.jpg" alt="ARViz GUI and Data Visualization">
    </a>
</p>

### TFs performance demonstration
<!--
[![ARViz TF2 demo](http://img.youtube.com/vi/QVhvxE6DuYM/0.jpg)](https://www.youtube.com/watch?v=QVhvxE6DuYM)
-->
<p align="center">
    <a href="https://www.youtube.com/watch?v=QVhvxE6DuYM">
        <img src="http://img.youtube.com/vi/QVhvxE6DuYM/0.jpg" alt="ARViz TF2 demo">
    </a>
</p>

### AR UI - Topics and Types
<!--
[![ARViz UI](http://img.youtube.com/vi/Av-UpGzqmOc/0.jpg)](https://www.youtube.com/watch?v=Av-UpGzqmOc)
-->
<p align="center">
    <a href="https://www.youtube.com/watch?v=Av-UpGzqmOc">
        <img src="http://img.youtube.com/vi/Av-UpGzqmOc/0.jpg" alt="ARViz UI">
    </a>
</p>


***
<p align="center">
    <strong>Intelligent Robotics</strong> -  
    <a href="http://intelligentrobotics.es/"> intelligentrobotics.es </a> - 
    <a href="https://twitter.com/IntellRobotLabs"> @IntellRobotLabs </a>
</p>
