# Radio Station Scheduling System

## 1. Overview
This project involves creating a scheduling system for a radio station that operates 24/7. The station's content consists of:
- Live sessions
- Pre-recorded material
- Music (default content)

## 2. Core Requirements

### 2.1 Schedule Management
- Schedule maintained 7 days in advance
- Days align with calendar (00:00 - 23:59)
- Continuous 24-hour broadcasting
- Default content: Music plays when no other content is scheduled

### 2.2 Content Types
1. Pre-recorded reportage
2. Live studio sessions
   - Single host: Uses Studio 1 (lower cost)
   - Multiple participants: Uses Studio 2
   - Guest appearances incur additional transport costs

### 2.3 Technical Implementation
- Use strong typing (classes) for all scheduled data
- Schedule structure: Implement as list of lists
- Future considerations:
  - Economic tracking may be added later
  - Web application integration

## 3. API Endpoints

### 3.1 Schedule Operations
- Get today's schedule
- Get 7-day schedule
- Get single event details
- Post new event
- Reschedule existing event

### 3.2 Participant Management
- Add/Remove host
- Add/Remove guest

### 3.3 Event Management
- Delete scheduled event

## 4. Technical Notes
- Use MapGet() for retrieving information
- Use MapPost() for modifying information
- Implementation should support future refactoring

## 5. Project Deadline
Submit to GitHub repository by Tuesday, September 23, 15:00

Note: This is a web application project with focus on API implementation and scheduling logic.
