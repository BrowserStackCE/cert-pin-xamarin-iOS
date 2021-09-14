const express = require("express");
const { readFileSync } = require("fs");
const https = require("https");

const app = express();

app.get("/", (_, res) => {
	res.send("<html><body><h1>Hello World!</h1></body></html>");
});

https
	.createServer(
		{
			key: readFileSync("./server.key"),
			cert: readFileSync("./server.cert"),
		},
		app
	)
	.listen(8000, () => console.log("Started listening.."));
