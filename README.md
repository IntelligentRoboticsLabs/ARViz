# ARViz

Intelligent Robotics http://intelligentrobotics.es/ @IntellRobotLabs 

ARViz is an RViz implementation for Augmented Reality. This application runs inside the Microsoft Hololens AR device as a UWP application, without the need to use Ros Bridge. This project has as goal to combine:

* ROS2 C # support for UWP applications on board Microsoft Hololens.
* TF2 support for ROS C#.
* Secure communications.
* Hololens Positioning in the scene.

This project is based on the works of Esteve Fernández (esteve@apache.org) collected in the repository https://github.com/esteve/ros2_dotnet. You can see his [Talk](https://vimeo.com/293302046) in ROSCon 2018 about this.

This project is funded by [ROSIN](http://rosin-project.eu/) as a focused Technical Project.

Project mantainers:
* David Vargas (dvargas@inrobots.es)
* Francisco Martín (fmrico@gmail.com)

Advisor:
* Esteve Fernández (esteve@apache.org)

## Compilation of ROS2 C# UWP 

### Compiling ament 

```md \dev\ament\src
cd \dev\ament
curl -sk https://raw.githubusercontent.com/esteve/ros2_dotnet/master/ament_dotnet_uwp.repos -o ament_dotnet_uwp.repos
vcs import src < ament_dotnet_uwp.repos
python src\ament\ament_tools\scripts\ament.py build --cmake-args -G "Visual Studio 15 2017 Win64" --
```

### Compiling ROS2 with C# for UWP32

```md \dev\ros2_uwp\src
cd \dev\ros2_uwp
curl -sk https://raw.githubusercontent.com/esteve/ros2_dotnet/master/ros2_dotnet_uwp.repos -o ros2_dotnet_uwp.repos
vcs import src < ros2_dotnet_uwp.repos
cd \dev\ros2_uwp\src\ros2_dotnet
vcs custom --git --args checkout master || VER>NUL
cd \dev\ament
call install\local_setup.bat
cd \dev\ros2_uwp
ament build --cmake-args -G "Visual Studio 15 2017" -DCMAKE_SYSTEM_NAME=WindowsStore -DCMAKE_SYSTEM_VERSION=10.0.14393 -DTHIRDPARTY=ON -DINSTALL_EXAMPLES=OFF -DCMAKE_FIND_ROOT_PATH="\dev\ament\install;\dev\ros2_uwp\install" 
```

### Compiling ROS2 with C# for Win64 

```md \dev\ros2_win64\src
cd \dev\ros2_win64
curl -sk https://raw.githubusercontent.com/esteve/ros2_dotnet/master/ros2_dotnet.repos -o ros2_dotnet.repos
vcs import src < ros2_dotnet.repos
cd \dev\ros2_win64\src\ros2_dotnet
vcs custom --git --args checkout master || VER>NUL
cd \dev\ament
call install\local_setup.bat
cd \dev\ros2_win64
ament build --cmake-args -G "Visual Studio 15 2017 Win64" -DTHIRDPARTY=ON -DINSTALL_EXAMPLES=OFF -DCMAKE_FIND_ROOT_PATH="\dev\ament\install;\dev\ros2_win64\install" 
```

### Using generated DLLs in your UWP application


