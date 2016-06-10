# NGauge

## Bridging the gap between NCrunch and Gauge

This software provides a bridge between [NCrunch](https://ncrunch.net/) and [Gauge](http://getgauge.io/), allowing NCrunch to execute your Gauge specifications in Visual Studio.

## About

### What is NCrunch?

NCrunch is an automated concurrent testing tool for Visual Studio.

It intelligently runs automated tests so that you don't have to, and gives you a huge amount of useful information about your tested code, such as code coverage and performance metrics, inline in your IDE while you type.

See [NCrunch](https://ncrunch.net/) for further information.

### What is Gauge?

Gauge is a light weight cross-platform test automation tool. It provides the ability to author test cases in the business language.

Gauge champions the idea of **living/executable documentation**.

See [Gauge](http://getgauge.io/) for further information.

### What is NGauge?

NGauge enables NCrunch to execute your Gauge tests, providing all the benefits of automated concurrent testing to your Gauge specifications.

### How does NGauge work?

NGauge uses the Gauge API to automatically generate bridging code between Gauge specifications and xUnit, allowing NCrunch to execute your Gauge specifications just like any other xUnit test. There are plans to introduce support for NUnit and MSTest, too!

NGauge will create and maintain an `NGauge` folder in each Visual Studio project that has Gauge specifications. This folder contains the automatically generated bridging code.

NGauge monitors Gauge's `.spec`, `.cpt` and `.md` files and automatically regenerates the bridging code when they are changed [saved].

## License

NGauge is released under Apache License 2.0

## Copyright

Copyright 2015 William Cowell Consulting Limited.