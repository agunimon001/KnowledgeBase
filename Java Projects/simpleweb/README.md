# Project: Simple WebApp

This project details how to start a simple web application using Spring Boot.

The latest Spring Boot version is 2.1.1 as of writing.

## Steps

1. Create new Maven Project.
2. Select maven-archetype-webapp to use.
> By default, the project would be using JRE1.5 for environment. Ignore for now.
3. Open pom.xml.
4. Edit Parent under Overview tab.
> You can also do it in its XML format, but just capitalize on what Eclipse has.
```
Group id:    org.springframework.boot		<< important to remember when doing Spring Boot project
Artifact id: spring-boot-starter-parent
> Spring Boot artifacts typically use spring-boot-starter-* to identify, so it's good to remember.
Version:     x.x.x.RELEASE
> Look at Spring.io for the latest version. Currently it's at 2.1.1.
```
5. Under Dependency tab, add spring-boot-starter-web.
> Group id is the same, and version can be left blank as it's handled by the parent POM.
> Version is needed only if the artifact is not handled by the parent POM.
6. Save the pom file.
7. Once ready, rebuild the Maven project.
> Shortcut is Alt+F5.


By now, all errors and warnings should have disappeared in your Markers window.
If you check your project property's Java Build Path, you would find that your Java used is changed to JavaSE-1.8. (At least mine is)
If you check under Java Compiler, you would also find that the compliance is now at 1.8 instead of 1.5.
If you still see some error or warning still in the Markers window, try restarting your IDE (File -> restart). Don't forget to rebuild project (make it a habit for Maven).

**If you are to use JavaFX, you'll need to change the JRE System Library to a JDK.**
> JavaFX is introduced in Java SE 7 Update 2, so using at least JDK 8 is good.
> Source: https://docs.oracle.com/javafx/2/installation/jfxpub-installation.htm

The next step is something I remember learning from some website which I've forgotten where.
I see it as a good practice.

8. Under src/main, create a folder "java".
> The new folder should also appear automatically under Java Resources. If it doesn't, right-click on it and click "Build Path -> Use as Source Folder".

```
Folder "java" should contain all your java back-end files.
Folder "resources" should contain all your resources.
Folder "webapp" should contain all your front-end files (e.g. html, jsp)

All three folders should be intuitively enough.
```

9. Create MainApp.java
10. Create RestMainController.java