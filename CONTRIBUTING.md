# Developing for NGauge

We welcome your contributions! Thanks for helping make NGauge a better project for everyone. Please review the backlog and discussion lists before starting work.  What you're looking for may already have been done. If it hasn't, the community can help make your contribution better. If you want to contribute but don't know what to work on, [issues tagged ready for work](https://github.com/wc2/NGauge/labels/ready%20for%20work) should have enough detail to get started.

## General Workflow

Please submit pull requests via feature branches using the semi-standard workflow of:

```bash
git clone git@github.com:yourUserName/NGauge.git               # Clone your fork
cd NGauge                                                      # Change directory
git remote add upstream https://github.com/wc2/NGauge.git      # Assign original repository to a remote named 'upstream'
git fetch upstream                                             # Pull in changes not present in your local repository
git checkout -b features/my-new-feature                        # Create your feature branch
git commit -am 'Add some feature'                              # Commit your changes
git push origin features/my-new-feature                        # Push to the branch
```

Once you've pushed a feature branch to your forked repo, you're ready to open a pull request. We favour pull requests with very small, single commits with a single purpose.

## Background

### Directory Structure

* `/src` contains all of the source files
    * `/src/Common` - Common files shared by projects
    * `/src/Libraries` - Library projects
    * `/src/Tests` - Tests, mirroring Libraries and Vsix
    * `/src/Vsix` - Visual Studio plug in

## Before Committing or Submitting a Pull Request

1. Ensure all tests are green
1. Ensure the Visual Studio plug in runs without issue in Visual Studio (Experimental instance)