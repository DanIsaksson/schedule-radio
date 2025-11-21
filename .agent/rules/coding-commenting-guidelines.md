---
trigger: always_on
---

---
trigger: always_on
---

System: # Coding & Commenting Knowledge Base – Usage Guide

This guide describes how to leverage the [coding-commenting-professional-guidelines/](cci:7://file:///f:/Code-Projects/00-Study-00/Lexicon/Weeks/Week%2017-18/Assignment/Scheduler-Radio-Antigrav/coding-commenting-professional-guidelines:0:0-0:0), including understanding the relationship between **YAML manifests** and markdown topic files. It is designed to help both humans and tools/AI use the resource as a structured knowledge base.

---

## 1. Folder & Domain Overview

All professional guidelines reside in:

- [coding-commenting-professional-guidelines/](cci:7://file:///f:/Code-Projects/00-Study-00/Lexicon/Weeks/Week%2017-18/Assignment/Scheduler-Radio-Antigrav/coding-commenting-professional-guidelines:0:0-0:0)
  - [0-manifests/](cci:7://file:///f:/Code-Projects/00-Study-00/Lexicon/Weeks/Week%2017-18/Assignment/Scheduler-Radio-Antigrav/coding-commenting-professional-guidelines/0-manifests:0:0-0:0) – YAML **indexes** of topics grouped by domain.
  - Domain folders with markdown content: [architecture/](cci:7://...), [design-principles/](cci:7://...), [formatting-structure/](cci:7://...), [functions-objects/](cci:7://...), [general/](cci:7://...), [naming-communication/](cci:7://...), [process-practices/](cci:7://...), [testing-quality/](cci:7://...).

**Workflow:**
- Do **not** scan all markdown files blindly.
- **Always** start with the manifests; only open files specifically listed therein.

---

## 2. Manifest Files (YAML)

Each manifest in [0-manifests/](cci:7://...) (e.g. [general.yaml](cci:7://...), [naming-communication.yaml](cci:7://...)) outlines topics within its domain, for example:

```yaml
domain: naming-communication
topics:
  - id: kb-naming-communication-001
    title: "- CI/CD – **3.2 Bad comments**"
    intent: "Short description of what this topic covers and why."
    tags:
      - comments
      - naming
    file: "naming-communication/ci-cd-3-2-bad-comments.md"
    source_lines:
      start: 311
      end: 4427
    domain: naming-communication
```

**Each topic entry includes:**
- `id`: Stable identifier (e.g., kb-naming-communication-001)
- `title`: Human-readable heading
- `intent`: Short summary
- `tags`: Keywords (naming, comments, guideline, testing, etc.)
- `file`: Path to corresponding markdown topic file
- `source_lines.start/end`: Original location in the source

The manifests in `0-manifests/` are the single source of truth.

---

## 3. Topic Files (Markdown)

For each topic in a manifest, `file` points to a markdown file in the relevant domain folder (e.g., `general/clean-code-fundamentals.md`, `formatting-structure/ci-cd-2-6-information-hiding.md`).

Typically, each topic file includes:
- A short header
- An excerpt from Clean Code Fundamentals
- Occasionally, images from the `images/` directory

**Summary:**
- **Manifests:** Index
- **Topic files:** Canonical guideline text

---

## 4. Human Workflow

To use the guidelines as a developer:
1. **Pick a domain:**
    - Naming/comments → `0-manifests/naming-communication.yaml`
    - Formatting/information hiding → `0-manifests/formatting-structure.yaml`
    - Functions/classes → `0-manifests/functions-objects.yaml`
    - Testing/TDD → `0-manifests/testing-quality.yaml`
    - Process/CI/CD/refactoring → `0-manifests/process-practices.yaml`
    - Architecture/high-level design → `architecture.yaml`, `design-principles.yaml`
2. **Scan the manifest**: Use `title`, `intent`, and `tags` to identify relevant topics.
3. **Open the topic file** as specified by the manifest.
4. **Read** the markdown content from that file.
5. **Translate** examples and suggestions to your language/framework as needed.
6. **Apply guidance** incrementally and safely.
7. **Use the Boy Scout Rule:** Whenever you touch code, make it slightly better using these topics.

---

## 5. How Tools and AI Should Use the Knowledge Base

- Load manifests from the `0-manifests/` directory; each YAML file is an index for one domain.
- Filter topics by:
    - `tags` (e.g., naming, comments, testing, guideline, process)
    - `intent` (free-text search)
- Open topic markdown files using the given file path; treat their content as authoritative guidance.
- **Anti-hallucination rule:**
    - All advice must be traceable to at least one topic `id` and file.
    - If advice is not directly supported by a topic or compatible tag, label it as speculative.

---

## 6. Domain Cheat Sheet (Quick Reference)
- `general`: Big-picture context for Clean Code Fundamentals
- `naming-communication`: Names, comments, intent-revealing code
- `formatting-structure`: Layout, information hiding, structure
- `functions-objects`: Function size, responsibilities, object design
- `testing-quality`: Testing, TDD, quality measures
- `process-practices`: Refactoring, CI/CD, continuous improvement
- `architecture`, `design-principles`: High-level design, patterns
- `domain`: Matches the manifest's domain