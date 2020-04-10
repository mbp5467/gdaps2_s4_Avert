# IGME-106 Spring 2020 
# Team All Valuable Electronics Restart Time (AVERT)

_This README contains basic information about the project and its artifacts. If kept up to date, it will act as a primary dashboard for the project._

## Team

- Matthew Palermo
- Collin Strauch
- Tracy Fu
- Tommy Murray Jr.

## Goals
The primary goals of this project are the following:
1. Practice with the development of a larger project over the course of a semester
2. To gain experience working as a part of a small team
3. To implement a variety of game-related algorithms 
4. To implement data structures for a game 
5. To have fun making a game

## Mission Statement
_Replace this with the overall objectives that your team has agreed upon._
_What is the goal of the project (from a learning and product perspective)? Who is your target audience? How will you define success?_
The goal of the project from a learning and product perspective is for everyone on the team to have a firsthand team-based experience in game development. Since the industry in game development is very team-heavy, learning how to make a gamer in teams early is very helpful in learning how it works in the industry, as well as learning potential team skills that would be helpful going forward. The target audience of the game we are creating, titled Avert, is to anyone who enjoys puzzle games. We consider success to be a game that's playable and has several levels of puzzles with different solutions. If we can accomplish that, then the game has made its purpose and it would be a success.

## Communications

### Discord
_Add info about how to join/use your team's Discord channel. What is it?_
The entire team is keeping track of Discord in the general channel only. It's purpose is to coordinate meetings, as well as ask teammates for clarification if someone is working alone.

### Meeting Times
_Set at least 1 out-of-class meeting time and note times when the entire group is usually free for additional meetings as needed._
Wednesdays at 12PM over a Discord voice call.

## Repository Overview
_As the project progresses, add notes here about how the repository is organized, how branches are being used, etc._
The repository and branches are mainly being used to keep track of which tasks have been accomplished before they were pushed. If the team needs to check back to previous code in order to help them understand a topic that was used during the development, we would look back at previous repository pushes and respective branches in order to compare the output with what we are trying to accomplish.

**Instructions about the required documentation/activities for each milestone can be found in [instructions/](instructions/Milestone Instructions.md).**

| File/Directory | Contents |
| -------------- | ----------- |
| [Google Documentation](TBD) | https://docs.google.com/document/d/1nSW5v7hB6X4PuBVPtCtv6lNH2kY1LTXddgQnLUTfOec/edit?ts=5e3848da#heading=h.t0sfhbwed8te
| [game/ReleaseNotes.md](src/ReleaseNotes.md) | MonoGame release notes _Update this with each milestone._|
..Milestone 2 expanded on the implementation greatly by implementing the external tool, and all of the code for the child classe needed from their respective abstract classes.
| [external_tool/ReleaseNotes.md](src/ReleaseNotes.md) | External tool release notes _Update this with each milestone._| 
...The implementation of the external tool for milestone 2 has started. It loads a level with a .txt file, and can be tested when the player tries to load a level. However, this only counts for a tutorial level and not for any physically playable stages.
...A bug regarding the external tool takes place with the stable shapes. The data from the .txt file is supposed to load the stable shapes inside the level, however the stable shape always returns null. For now the stable shapes had to be hard-coded.

## Other Resources
- [MonoGame Documentation](http://www.monogame.net/documentation/?page=main)
- [Course homepage](https://esmesh.github.io/RIT-IGME-106/)
- This [UML class diagram reference](https://www.uml-diagrams.org/class-reference.html) probably has more than you'll need, but it's a good starting point.
- [Markdown](https://docs.gitlab.com/ee/user/markdown.html)
