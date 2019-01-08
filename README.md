# ARViz

Intelligent Robotics http://intelligentrobotics.es/ @IntellRobotLabs 

ARViz is an RViz implementation for Augmented Reality. This application runs inside the Microsoft Hololens AR device as a UWP application, without the need to use Ros Bridge. This project has as goal to combine:

* ROS2 C # support for UWP applications on board Microsoft Hololens.
* TF2 support for ROS C#.
* Secure communications.
* Hololens Positioning in the scene.

This project is based on the [UWP port of ROS2](https://github.com/esteve/ros2_dotnet) done by [Esteve Fernández](https://github.com/esteve). You can see the talk he gave at ROSCon 2018 about ROS2 for Android, iOS and UWP [Video](https://vimeo.com/293302046) [Slides](https://roscon.ros.org/2018/presentations/ROSCon2018_ROS2%20for%20Android,%20iOS%20and%20Universal%20Windows%20Platform.pdf).

This project is funded by [ROSIN](http://rosin-project.eu/) as a focused Technical Project.

Project mantainers:
* David Vargas (dvargas@inrobots.es)
* Francisco Martín (fmrico@gmail.com)

Advisor:
* Esteve Fernández (esteve@apache.org)

## Compilation of ROS2 C# UWP 

Follow the instructions on [ros2-dotnet](https://github.com/esteve/ros2_dotnet) to compile [ROS2 for UWP](https://github.com/esteve/ros2_dotnet/blob/master/README.md#universal-windows-platform-arm-win32-win64)

### Using generated DLLs in your UWP application


