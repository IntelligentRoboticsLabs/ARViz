# ARViz

Intelligent Robotics http://intelligentrobotics.es/ @IntellRobotLabs 

ARViz in an RViz implementation for Augmented Reality. This application runs inside the Microsoft Hololens AR device as a UWP application, without the need to use Ros Bridge. This project has as goal to combine:

* ROS2 C # support for UWP applications on board Microsoft Hololens.
* TF2 support from ROS C#.
* Secure communications.
* Hololens Positioning in the scene.

This project is based on the works of Esteve Fernández (esteve@apache.org) collected in the repository https://github.com/esteve/ros2_dotnet . This project is funded by [ROSIN](http://rosin-project.eu/) as a focused Technical Project.

Project mantainers:
* David Vargas (dvargas@inrobots.es)
* Francisco Martín (fmrico@gmail.com)

Advisor:
* Esteve Fernández (esteve@apache.org)

## Compilation of ROS2 C# UWP DLLs

### Compiling ament 

```md \dev\ament\src
cd \dev\ament
curl -sk https://raw.githubusercontent.com/esteve/ros2_dotnet/master/ament_dotnet_uwp.repos -o ament_dotnet_uwp.repos
vcs import src < ament_dotnet_uwp.repos
python src\ament\ament_tools\scripts\ament.py build --cmake-args -G "Visual Studio 15 2017 Win64" --
```

### Compiling ROS2 with C# for UWP 

```md \dev\ros2\src
cd \dev\ros2
curl -sk https://raw.githubusercontent.com/esteve/ros2_dotnet/master/ros2_dotnet_uwp.repos -o ros2_dotnet_uwp.repos
vcs import src < ros2_dotnet_uwp.repos
cd \dev\ros2\src\ros2_dotnet
vcs custom --git --args checkout master || VER>NUL
cd \dev\ament
call install\local_setup.bat
cd \dev\ros2
ament build --cmake-args -G "Visual Studio 15 2017" -DCMAKE_SYSTEM_NAME=WindowsStore -DCMAKE_SYSTEM_VERSION=10.0.14393 -DTHIRDPARTY=ON -DINSTALL_EXAMPLES=OFF -DCMAKE_FIND_ROOT_PATH="\dev\ament\install;\dev\ros2\install" 
```

### Using generated DLLs in your UWP application


