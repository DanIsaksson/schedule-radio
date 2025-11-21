---
id: kb-testing-quality-010
title: "**WBT** **BBT** – **4.9 Continuous Integration**"
domain: testing-quality
parent_heading: "**4. Software Quality Assurance**"
intent: "**WBT** **BBT** – **4.9 Continuous Integration**: Continuous integration describes a process of regular, complete rebuilding, testing and distribu[tion of an application. There are cloud providers like CircleCI³⁸ or Travis³⁹ or local servers like](https://circleci.com/) [Jenkins⁴⁰, GitLab CI⁴¹, TeamCity⁴² and Bamboo⁴³.](https://www.jenkins.io/) ![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-121-0.png)"
tags:
  - architecture
  - classes
  - clean-code
  - comments
  - functions
  - guideline
  - principles
  - process
  - testing
source_lines:
  start: 4688
  end: 7894
---
## **WBT** **BBT**
### **4.9 Continuous Integration**


Continuous integration describes a process of regular, complete rebuilding, testing and distribu[tion of an application. There are cloud providers like CircleCI³⁸ or Travis³⁹ or local servers like](https://circleci.com/) [Jenkins⁴⁰, GitLab CI⁴¹, TeamCity⁴² and Bamboo⁴³.](https://www.jenkins.io/)

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-121-0.png)


```
#### Continuous Integration


#### Idea:


  - Early and more frequent check-in of changes into the version management system.

  - Replacement of large changes with incremental, functional small changes.

  - When changes are checked into the version management system, the entire system is rebuilt
and automatically tested.

  - Try to give the developer feedback on his changes as soon as possible.

  - Improve quality by replacing the traditional approach to testing with continuous testing
and rapid feedback.


#### Parts and steps:


  - Compilation

  - Test execution (Unit tests, Integration tests, Acceptance tests, etc.)

  - Integration (Database, Third-party systems)

  - Static analysis (Code & Architecture)

  - Automatic deployment

  - Generation of documentation


³⁸ [https://circleci.com/](https://circleci.com/)
³⁹ [https://travis-ci.org/](https://travis-ci.org/)
⁴⁰ [https://www.jenkins.io/](https://www.jenkins.io/)
⁴¹ [https://docs.gitlab.com/ee/ci/](https://docs.gitlab.com/ee/ci/)
⁴² [https://www.jetbrains.com/teamcity/](https://www.jetbrains.com/teamcity/)
⁴³ [https://www.atlassian.com/software/bamboo](https://www.atlassian.com/software/bamboo)


```
#### Addressed risks:


  - Late troubleshooting

  - Lack of team coordination


#### –
_Your changes don’t match mine_

#### –
_Didn’t you fix this two months ago?_

  - Poor code quality


#### –
_Why do three different classes do the same thing?_

#### –
_The code of Team XY looks completely different_

  - Lack of transparency/visibility


#### –
_What tests aren’t running?_

**–** _What does build 1.2.3 contain?_

#### –
_Where do we stand with code coverage?_

  - Software not available


#### –
_I’m all right_

#### –
_It’s working_

**–** _I need another build to test_

#### –
_Tomorrow the boss is coming, we need a demo_

#### **4.9.1 Differences between CI, CD, and CD**

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-122-0.png)


#### Differences between CI, CD and CD


#### Continuous Integration


  - With CI, code changes are tested immediately after check-in, so that the newly included
changes can be tested continuously.

  - The basic idea is to test the code frequently during the development process to detect and
fix potential problems early on.

  - With CI, most of the work is done by automated tests and a build server.

  - Since CI provides constant feedback on the software status, code problems that arise are
usually less complex and much easier to solve.


#### Continuous Delivery


  - Continuous Delivery is an extension of the CI. It involves the Continuous Delivery of
developed code to a staging environment as soon as the developer has developed it.

  - It includes Continuous Integration, test automation, and deployment automation processes
that enable fast and reliable software development and deployment with minimal manual effort.

  - The core idea is to deliver the code to QA, customers, or any user base so that it can be
continuously and regularly reviewed.

  - This makes it possible to identify problems early in the development cycle and ensures that
they can be resolved early.


#### Continuous Deployment


  - Continuous Deployment is the next logical step after Continuous Delivery.

  - It is the process of delivering code directly into production once it has been developed.

  - With Continuous Deployment, all changes that pass the automated tests are automatically
transferred to production.

  - Successful implementation requires the automation of Continuous Integration, as well
as the automation of Continuous Delivery into the staging environment. Finally, it also requires the ability to automatically deploy to production.


```
#### **4.9.2 CI Workflow**

![](F:/Code-Projects/00-Study-00/00-Book-Referencing-00/Clean Code Fundamentals - Martin Hock-md/images/clean-code-understand-fundamentals.pdf-124-0.png)


#### Continuous Integration Workflow


In your CI workflow, you can integrate many useful tools depending on what your goal is. In
this case, the following tools are part of the CI workflow.


[Nexus](https://www.sonatype.com/nexus) ⁴⁴


Manage binaries and build artifacts across your software supply chain.
[SonarQube](http://www.sonarqube.org/) ⁴⁵

Platform for static code analysis of the technical quality. Supports not only Java but also the
analysis of other programming languages. Analyses the code for different quality ranges
and displays the results graphically.
[Renovate](https://github.com/renovatebot/renovate) ⁴⁶


Automated dependency updates for multi-platform and multi-languages.
[DependencyTrack](https://dependencytrack.org/) ⁴⁷

Platform that allows organizations to identify and reduce risk from the use of third-party
and open-source components.
[Naikan](https://naikan.io/) ⁴⁸


Naikan is a software inventory management tool driven by our CI/CD pipeline.


⁴⁴ [https://www.sonatype.com/nexus](https://www.sonatype.com/nexus)
⁴⁵ [http://www.sonarqube.org/](http://www.sonarqube.org/)
⁴⁶ [https://github.com/renovatebot/renovate](https://github.com/renovatebot/renovate)
⁴⁷ [https://dependencytrack.org/](https://dependencytrack.org/)
⁴⁸ [https://naikan.io](https://naikan.io/)


```
#### **4.9.3 Preconditions**


#### Common code base


  - To be able to integrate meaningfully within a team, a version management system must
exist in which all developers can continuously integrate their changes.


#### Automated compiling


  - Every integration must pass through uniformly defined tests before the changes are
integrated.

  - This requires automated compiling.


#### Continuous test development


  - Each change should be developed with an associated test if possible.

  - Newly developed tests should become part of the automated test suite.

  - Use code coverage to document and control test coverage.


#### Frequent integration


  - Integrate changes into the common code base as often as possible.

  - The risk of failed integrations is minimized with small increments.

  - Developer’s work is often secured in the common code base.


#### Short test cycles


  - Test cycle before integration should be short to encourage frequent integration.


#### Mirrored production environment


  - The changes should be tested in a replica of the real production environment.


#### Easy access


  - Even non-developers need easy access to the results of software development.


#### Automated reporting


  - When was the last successful integration?

  - What changes have been introduced since the last delivery?

  - What is the quality of the version?


#### Automated distribution


#### **4.9.4 Advantages and Disadvantages**


  - Integration problems are constantly being identified and resolved, not just before a
milestone

  - Early warnings for mismatched components

  - Quick detection of errors through immediate execution of unit tests

  - Constant availability of an executable stand for demo, test, or distribution purposes

  - Fast feedback on the check-in of incorrect or incomplete code

  - Early and frequent testing, errors are found earlier

  - Tests take place in parallel with the development

  - Correction of errors at the earliest, most favorable time

  - Ability to react immediately to key metrics

  - Review of _coding standards_ and _best practices_


  - Setting up the infrastructure

  - Costs for hardware and software

#### **4.9.5 Best Practices**


  - Use of a version management system

  - Committing changes to the main branch should automatically trigger a build

[• Check-in early and often, prefer Trunk Based Development⁴⁹](https://trunkbaseddevelopment.com/)

  - Do not check in a code that is not running

  - Fix build errors immediately

  - Tests should run as part of the CI process, with test failures causing a build failure

  - Tackling problems early and failing quickly

  - Code quality checks should run as part of the CI process

  - Act and react based on metrics

  - Create artifacts for each build

  - Use configuration as code, the CI server configuration should be maintained as code

  - Provide fast builds, so developers run these tests locally often


⁴⁹ [https://trunkbaseddevelopment.com/](https://trunkbaseddevelopment.com/)


# **5. Design Principles**
