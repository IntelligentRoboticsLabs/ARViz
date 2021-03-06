﻿/*********************************************************************
 * Software License Agreement (BSD License)
 *
 *  Copyright (c) 2018, Intelligent Robotics Core S.L.
 *  All rights reserved.
 *
 *  Redistribution and use in source and binary forms, with or without
 *  modification, are permitted provided that the following conditions
 *  are met:
 *
 *   * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *   * Redistributions in binary form must reproduce the above
 *     copyright notice, this list of conditions and the following
 *     disclaimer in the documentation and/or other materials provided
 *     with the distribution.
 *   * Neither the name of Intelligent Robotics Core nor the names of its
 *     contributors may be used to endorse or promote products derived
 *     from this software without specific prior written permission.
 *
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 *  "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 *  LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
 *  FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
 *  COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
 *  INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 *  BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 *  LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
 *  CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 *  LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
 *  ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 *  POSSIBILITY OF SUCH DAMAGE.
 **********************************************************************/

/* Author: David Vargas Frutos - dvargasfr@gmail.com */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ROS2;
using ROS2.TF2;

public class CameraPositionPublisher : MonoBehaviour
{
    INode node;
    TransformBroadcaster tfbr_;
    std_msgs.msg.String msg;

    void Start()
    {
        RCLdotnet.Init();
        node = RCLdotnet.CreateNode("camera_pos_publisher");
        Debug.Log("########## CameraPositionPublisher TransformBroadcaster 1");
        tfbr_ = new TransformBroadcaster(ref node);
        Debug.Log("########## CameraPositionPublisher TransformBroadcaster 2");

        // Invokes the method PublishHololensCamera2HololensInit every second
        InvokeRepeating("PublishHololensCamera2HololensInit", 0, 0.1f);
    }
    
    // void Update()
    void PublishHololensCamera2HololensInit()
    {
        if (RCLdotnet.Ok())
        {
            geometry_msgs.msg.TransformStamped w2h = new geometry_msgs.msg.TransformStamped();
            
            w2h.Header.Frame_id = "hololens_camera_init";

            System.Tuple<int, uint> ts = RCLdotnet.Now();

            w2h.Header.Stamp.Sec = ts.Item1;
            w2h.Header.Stamp.Nanosec = ts.Item2;
            w2h.Child_frame_id = "hololens_camera";
            
            w2h.Transform.Translation.X = Camera.main.transform.position.z;
            w2h.Transform.Translation.Y = -Camera.main.transform.position.x;
            w2h.Transform.Translation.Z = Camera.main.transform.position.y;
            w2h.Transform.Rotation.X = Camera.main.transform.rotation.z;
            w2h.Transform.Rotation.Y = -Camera.main.transform.rotation.x;
            w2h.Transform.Rotation.Z = Camera.main.transform.rotation.y;
            w2h.Transform.Rotation.W = -Camera.main.transform.rotation.w;
            /*
            w2h.Transform.Translation.X = Camera.main.transform.position.x;
            w2h.Transform.Translation.Y = Camera.main.transform.position.z;
            w2h.Transform.Translation.Z = Camera.main.transform.position.y;
            w2h.Transform.Rotation.X = -Camera.main.transform.rotation.x;
            w2h.Transform.Rotation.Y = -Camera.main.transform.rotation.z;
            w2h.Transform.Rotation.Z = -Camera.main.transform.rotation.y;
            w2h.Transform.Rotation.W = Camera.main.transform.rotation.w;
            */
            tfbr_.SendTransform(ref w2h);
        }
    }
}
