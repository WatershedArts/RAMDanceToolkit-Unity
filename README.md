RAMDanceToolkit-Unity
===

A simple project that allows users to bring data from the MOTIONER kit into Unity.

###Introduction
In Febuary 2016, we ran a [three day research lab](http://www.watershed.co.uk/studio/news/2016/02/17/hula-hoops-and-monsters-playing-motion-capture/) using the [MOTIONER and RAMDanceToolkit](http://special.ycam.jp/ram/tools/) developed by the YCAM. 

We currently have a full Motioner Kit on long term loan from the YCAM, to continue investigating how artists and technologists might use the system.

As part of this investigation we are developing a number of different applications and assets for people to use.

![Demo.png](./images/Demo.png)

####How to Setup
* Follow the Motioner Setup instuctions. (ie Creating a New Network)
* Run the Motioner Application.
* Disable OSC out
* Connect the Motioner Hardware
* Name your Motioner Actor
* If you the Unity Application is on a separate machine to the Motioner find its IP address. 
* Enter the IP into the Motioner's GUI as the Recevier Machine.
* Re-enable the OSC Out
* Run the Unity Scene
* If all is well, you should see the default Actor disappear and a new Actor appear with the Motioner's Name attached.

####How to Use
This is a basic example for the time being.

But say you want to be able to grab the left hand coordinates and print them to the console.
In update beneath the main loop do the following:

<pre>
<code>if (actors.ContainsKey("DefaultRAMActor"))
{
	Debug.Log(actors["DefaultRAMActor"].GetLimbCoordinates("HEAD"));
}
</code>
</pre> 

###Current Features

The current system is able to:

* Generate New Actors
* Move virtual actors.
* Recognise when new actors enter and exit.
* Abilty to interact with scene objects 

####Classes 

Current Classes:

* RAMActorManager
* RAMActor
* CameraControl
* OscConnection

###To Do

* Fix Exit Methods

###Built With

C#

Huge thanks to Phil Tew and his [Rug.Osc](https://bitbucket.org/rugcode/rug.osc)
