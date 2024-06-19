This is a coding challenge example of something written in C#. This can be run via the play button within VS.

# Prompt

An imaginary company stores a list of software products they use. They have stored the name and version of each product. They have asked us to create a simple website where users can type in a version number and receive a list of software products that are greater than the version they entered. If the user enters an invalid version, they should be notified that the version is not valid.

The software versions are stored as a string in a custom version format which has 5 parts stored as the format [major].[minor].[patch].[build].[compilation]. Each version part will always be non-negative integers. You may see versions like “2”, “1.5”, or “2.12.4.0.6”. The period is only used as a separator and does not represent a decimal point – 1.5 does not mean one and a half.

- "2" == "2.0" == "2.0.0" == "2.0.0.0" == "2.0.0.0.0"
- "2" < "2.0.0.0.1"
- "2" < "2.1"
- "2.0.1" < "2.1.0"

The imaginary company stores the software list as a C# object (provided below) that you can simply drop into your code – no need to call a database or REST service. This list of software is just a sample, assume the company will eventually switch to getting list from a database with thousands of softwares.

This site will be publicly available, so user authentication will not be required.
