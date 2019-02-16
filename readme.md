# 'Shooter game' tute

- following http://www.monogame.net/documentation/?page=Tutorials
- using linux + rider + dotnet instead

# getting started

to create this project from scratch, do

```
# install monogame. See monogame website for how to.
dotnet new -i "MonoGame.Template.CSharp"
mkdir ShooterToot3
dotnet new mgdesktopgl
# Change the namespace of Program to ShooterToot3 (otherwise build fails)
dotnet run
```

You should see a new window open with a blue background.


# Notes

- To add game assets, you need monodevelop with the monogame extension. Once
  You've got it, open it, and double click on Content/Content.mgcb. This will 
  open a 'content editor' of sorts, where you can add images. Once images are
  added here, they will be built into xnb files that monogame can use at runtime.
