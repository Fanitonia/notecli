## NoteCLI
[![NuGet][nuget-badge]][nuget]

A command line tool for basic note-taking. Built with .NET Core.

## Getting Started

### Prerequisites

* .NET

### Installation
* Install the [Tool][nuget] as a global tool.
```
$ dotnet tool install --g notecli
```

## Usage
```
(1.1.0)
Usage: note [command] [options]

Commands:
  add [YOUR NOTE]        Create new note.
  delete [NOTE ID]       Delete a note.
  update [NOTE ID]       Update note text.
  finish [NOTE ID]       Mark a note as finished.
  unfinish [NOTE ID]     Unmark the note.
  list [OPTIONS]         List all notes.
  help                   Get help about usage.

Options:
  --all                  Show all notes including finished
```


[nuget]: https://www.nuget.org/packages/notecli/
[nuget-badge]: https://img.shields.io/nuget/v/notecli