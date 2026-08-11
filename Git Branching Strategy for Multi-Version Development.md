# Git Branching Strategy for Multi-Version Development

The project requires a branching strategy that supports:

- Parallel development of multiple versions
- Experimental V2 development
- Urgent production hotfixes
- Collaboration between multiple developers
- Clear separation between production and development code
- Permanent preservation of released versions

## 🗺️ Recommended Strategy: GitFlow (Adapted)

The recommended approach is a simplified **GitFlow-style strategy** adapted for this project.

```text
main (production - v1.1)
│
├── hotfix/urgent-fix
│   └── merges back to main + v2-dev
│
└── v2-dev (experimental V2)
    │
    ├── feature/feature-a
    │
    └── feature/feature-b
```

---

# 📐 Branch Structure

| Branch | Purpose | Who Pushes |
|---|---|---|
| `main` | Production-ready code (`v1.1+`) | After review only |
| `v2-dev` | Experimental V2 development base | Both developers |
| `feature/*` | Individual features for V2 | Both developers |
| `hotfix/*` | Urgent fixes for production | Both developers |

### Branch Responsibilities

### `main`

The `main` branch always represents **production-ready code**.

- Contains released versions only
- Never push directly
- Changes should go through Pull Requests or controlled merges
- Every production release receives a Git tag

### `v2-dev`

The `v2-dev` branch is the integration branch for the next major version.

- Contains ongoing V2 development
- May temporarily contain unstable or experimental code
- Both developers can work against this branch
- Feature branches are created from this branch
- Never merge experimental V2 work directly into `main`

### `feature/*`

Feature branches isolate individual pieces of V2 development.

Examples:

```text
feature/new-dashboard
feature/api-refactor
feature/customer-search
feature/export-to-pdf
```

Feature branches:

- Start from `v2-dev`
- Are developed independently
- Are pushed to the remote repository
- Are merged back into `v2-dev` through Pull Requests

### `hotfix/*`

Hotfix branches are used for urgent production fixes.

They **always start from `main`**, because the problem exists in the production code.

Examples:

```text
hotfix/critical-payment-bug
hotfix/login-error
hotfix/database-timeout
```

After the fix is completed, it must be merged into:

1. `main`
2. `v2-dev`

This prevents the bug from being fixed in production but accidentally reintroduced in V2.

---

# 🚀 Initial Setup

## Step 1 — Tag the Current Production State

First, permanently mark the current production version.

```bash
git checkout main

git tag -a v1.1 -m "Release version 1.1 - production"

git push origin v1.1
```

This is critical.

The `v1.1` tag permanently identifies the exact state of the code that was released as version 1.1.

Even if `main` changes significantly later, you can always return to:

```text
v1.1
```

---

## Step 2 — Create the V2 Development Branch

Create the development branch from the current production state:

```bash
git checkout main

git checkout -b v2-dev

git push -u origin v2-dev
```

The repository now has:

```text
main
│
└── v2-dev
```

---

## Step 3 — Second Developer Checks Out V2

The second developer can retrieve the branch with:

```bash
git fetch origin

git checkout -b v2-dev origin/v2-dev
```

Or, with newer Git versions:

```bash
git switch --track origin/v2-dev
```

---

# 🔥 Scenario: Urgent Hotfix on V1.1

Suppose production is currently running version `v1.1` and an urgent bug is discovered.

## 1. Create the Hotfix Branch from `main`

**Do not branch from `v2-dev`.**

```bash
git checkout main

git checkout -b hotfix/critical-bug
```

Or:

```bash
git switch main
git switch -c hotfix/critical-bug
```

---

## 2. Fix and Commit the Bug

```bash
git add .

git commit -m "fix: resolve critical payment bug"
```

---

## 3. Merge the Fix into `main`

```bash
git checkout main

git merge --no-ff hotfix/critical-bug
```

Create the new production release tag:

```bash
git tag -a v1.2 -m "Release v1.2 - critical fix"
```

Push the updated production branch and tag:

```bash
git push origin main --tags
```

The production history is now:

```text
v1.1
 │
 └── hotfix
      │
      └── v1.2
```

---

## 4. ⚠️ Merge the Hotfix into `v2-dev`

This step is extremely important.

The same fix must be incorporated into V2:

```bash
git checkout v2-dev

git merge --no-ff hotfix/critical-bug

git push origin v2-dev
```

Otherwise, V2 could eventually reintroduce the same bug.

The resulting structure is:

```text
                 ┌── v1.1
                 │
main ────────────┴── v1.2
  \
   \
    v2-dev ────── hotfix fix
```

Conceptually, the fix exists in both development lines.

---

## 5. Clean Up the Hotfix Branch

Once the hotfix has been merged:

```bash
git branch -d hotfix/critical-bug

git push origin --delete hotfix/critical-bug
```

---

# 🧪 Scenario: Working on V2 Features

Both developers work on their own feature branches.

## Developer 1

Start from the latest `v2-dev`:

```bash
git checkout v2-dev

git pull origin v2-dev

git checkout -b feature/new-dashboard
```

Work on the feature and commit:

```bash
git add .

git commit -m "feat: add new dashboard"
```

Push the feature branch:

```bash
git push -u origin feature/new-dashboard
```

Then open a Pull Request:

```text
feature/new-dashboard
            ↓
         v2-dev
```

**Not `main`.**

---

## Developer 2

Similarly:

```bash
git checkout v2-dev

git pull origin v2-dev

git checkout -b feature/api-refactor
```

After development:

```bash
git add .

git commit -m "refactor: improve API structure"

git push -u origin feature/api-refactor
```

Then create:

```text
feature/api-refactor
            ↓
         v2-dev
```

---

# 🎯 Scenario: V2 Is Ready for Production

When V2 is complete, stable, and tested, it can be released.

## 1. Make Sure `v2-dev` Is Up to Date

```bash
git checkout v2-dev

git pull origin v2-dev
```

Run the required tests and verify that the branch is ready for release.

---

## 2. Merge V2 into `main`

```bash
git checkout main

git pull origin main

git merge --no-ff v2-dev -m "chore: merge v2-dev into main for v2.0 release"
```

---

## 3. Tag the New Release

```bash
git tag -a v2.0 -m "Release version 2.0"

git push origin main --tags
```

The repository now contains permanent release points:

```text
v1.1 ─── v1.2 ─── v2.0
```

The old versions are not lost.

Git tags permanently identify those historical commits.

---

# 🔑 Key Rules

The development team should agree on the following rules.

### 1. Never push directly to `main`

Production changes should go through a Pull Request or controlled merge.

### 2. Hotfixes always start from `main`

```text
main
 ↓
hotfix/*
```

Never start a production hotfix from `v2-dev`.

### 3. Hotfixes merge into both branches

After the fix:

```text
hotfix/*
   ├──→ main
   └──→ v2-dev
```

### 4. V2 features always start from `v2-dev`

```text
v2-dev
   ↓
feature/*
```

### 5. Tag every production release

Examples:

```text
v1.1
v1.2
v1.3
v2.0
v2.1
```

### 6. Keep `v2-dev` synchronized

Both developers should regularly pull the latest changes from `v2-dev` before starting or continuing feature work.

---

# 💡 GitHub Branch Protection

It is strongly recommended to protect both:

```text
main
v2-dev
```

In GitHub, configure branch protection rules to require:

- ✅ Pull Request before merging
- ✅ At least 1 approval
- ✅ Branches to be up to date before merging
- ✅ No direct pushes to protected branches

This provides an additional safety layer against accidentally pushing unfinished code to `main` or bypassing code review.

---

# 📊 Summary

| Concern | Solution |
|---|---|
| Keep V1.1 intact | Tag `v1.1` on `main` |
| Work on V2 separately | `v2-dev` branch |
| Develop individual V2 features | `feature/*` branches |
| Urgent production fix | `hotfix/*` from `main` |
| Collaborate on V2 | Feature branches + PRs into `v2-dev` |
| Don't lose previous releases | Git tags preserve release history |
| Prevent a hotfix from being lost in V2 | Merge hotfix into `v2-dev` after `main` |
| Protect production | Protect `main` on GitHub |
| Protect V2 integration | Protect `v2-dev` on GitHub |

---

# 🌳 Final Branching Model

The overall workflow can be visualized as:

```text
                         feature/A
                        /
                       /
v1.1 ──── main ────────┼──────────── v1.2 ────────────── v2.0
           \           /
            \         /
             \       /
              v2-dev
              /   \
             /     \
       feature/A   feature/B
             \     /
              \   /
              v2-dev


Hotfix flow:

main ──────── hotfix/critical-bug ────────→ main
                     │
                     └─────────────────────→ v2-dev
```

The key principle is:

> **Branches represent lines of development; tags represent released versions.**

`main` can move forward, `v2-dev` can evolve independently, and feature/hotfix branches can come and go. The release tags (`v1.1`, `v1.2`, `v2.0`, etc.) remain permanent references to the exact production state of each release.

This gives the team a simple workflow while still supporting parallel V2 development, production hotfixes, and multiple developers.