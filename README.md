## Description

Plain converter for Kindle notebooks. 
Convert kindle notebook from HTML to Markdown format removing all html styles, tags.

## Usage

### Arguments

- `-p` or `--path` - path to kindle notebook in HTML format
- `-o` or `--outputPath` - path to kindle notebook in MD format (optional)

### How to start using dotnet cli

```ps1
dotnet run -p "The Power of habit - Notebook.html"
```

### Installation

Navigate to KindleNoteConverter.Notebook.Console

#### Pack dotnet nuget package

```ps1
dotnet pack
```
#### Install package as dotnet global tool
```ps1
dotnet tool install --global --add-source ./nupkg KindleNoteConverter.Notebook.Console
```

##### Usage

It will be available from any place in console

```ps1
kindle -p "The Power of habit - Notebook.html"
```
