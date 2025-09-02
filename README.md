## NoteCLI
[![NuGet][nuget-badge]][nuget]

A command line tool for basic note-taking. Built with .NET Core.

## Getting Started

### Prerequisites

* .NET

### Installation
* Install the [NoteCLI][nuget] with dotnet.
```sh
$ dotnet tool install -g notecli
```

## Usage
```
(v1.3.0)
Usage: 
  note [notecli-options] 
  note [command] [command-options]

Commands:
  add <YOUR NOTE>        Create new note.
  delete <NOTE ID>       Delete a note.
  update <NOTE ID>       Update note text.
  done <NOTE ID>         Mark a note as done.
  undone <NOTE ID>       Unmark the note.
  list [options]         List all notes.
  clear [options]        Clear all your notes.
  help                   Get help about usage.

Options:
  --all | -a             Show all notes including done.
  --done | -d            Clear only done notes.
  --version | -v         Check the current version of notecli.
```


[nuget]: https://www.nuget.org/packages/notecli/
[nuget-badge]: https://img.shields.io/badge/nuget-v1.3.0-blue
