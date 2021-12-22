# cert-pin-xamarin-iOS

This repo contains an example application on how to pin your mobile native applications to use self signed certificates of your backend server without any additional configuration on the target device.

## Pre-requisites

* .NET Core
* NodeJS (for backend server)

## Setup

* Start the insecure API
	```sh
	cd insecure-api

	npm install # only need to run this for the first time to install dependencies
	
	node index.js
	```
	This should start the server on port 8000

* Navigate to [https://my.machine:8000](https://my.machine:8000). It should render "Hello World!". If it does not, then there is some problem with your installation

* Launch the app on a simulator or build an IPA for your devices

* The application should render a webview showing "Hello World!"
