# Moonpig Technical Assessment

Welcome to the Moonpig backend technical assessment. This exercise simulates a scenario you might encounter as part of our engineering team.

You are reviewing a pull request opened by a teammate (an Associate Engineer). Your task is to review their code as if it were their first pull request on a new project, providing constructive feedback to help them improve.

The feature requirements and acceptance criteria are outlined in [REQUIREMENTS.md](REQUIREMENTS.md).

## What we're looking for

This assessment is an important step in our hiring process. We expect candidates to approach the exercise thoughtfully and demonstrate their technical judgement. If the review shows little care or a lack of technical understanding, we may decide not to progress further.

We don’t expect perfection. What matters is your reasoning and how you communicate feedback.

When reviewing the pull request, we encourage you to:
- Identify issues that may cause problems (e.g. correctness, security, performance).
- Suggest improvements (e.g. readability, maintainability, clarity).
- Consider how you would phrase feedback to help a less experienced teammate grow.

> **Timebox:** Please spend around **1 hour** on this exercise. We value your time and don’t expect exhaustive coverage. Focus on what you believe matters most.

## Setup and Running

### Prerequisites
- .NET 8.0 SDK
- Node.js and npm

> If you haven’t already, trust .NET’s local HTTPS certs (required for `https://localhost:52641`):
```bash
dotnet dev-certs https --trust
```

### 1. Start the Mock Product API (`json-server`)

This simulates an external product service.

1. Install dependencies:
   ```bash
   npm install
   ```

2. Start the server (defaults: `http://localhost:3000`):
   ```bash
   npm start
   ```

   If port 3000 is busy, you can run it on another port:
   ```base
   npx json-server --watch db.json --port 3001
   ```

3. Verify it's running:
   ```bash
   curl http://localhost:3000/products   # macOS/Linux
   Invoke-WebRequest http://localhost:3000/products   # Windows PowerShell
   ```

### 2. Run the Basket API

1. Navigate to the API project directory:
   ```bash
   cd src/Moonpig.Basket.Api
   ```

2. Run the API:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7001` (or the port specified in `launchSettings.json`).

Quick check:
```bash
curl -k https://localhost:52641/api/basket/1   # macOS/Linux
Invoke-WebRequest https://localhost:52641/api/basket/1 -SkipCertificateCheck   # Windows PowerShell
```

(`-k`/`SkipCertificateCheck` ignores dev cert warnings.)

## Your Task

Review the pull request submitted by your teammate: [Pull Request #1](https://github.com/moonpig-spike/moonpig-spike-technical-assessment-backend/pull/1)

Provide feedback in the style of a GitHub review, focusing on clarity, correctness, and sound judgement.

### How to Answer

1. Make a copy of [PULLREQUEST.md](PULLREQUEST.md).
2. Add your review comments there, referencing file names and line numbers.
3. Email the completed file to your Moonpig contact.

## Criteria

We’ll assess on the quality of your points, how clearly you communicate them, and how well you balance criticism with guidance.