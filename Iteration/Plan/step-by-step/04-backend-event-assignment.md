# Step-by-step 04 – Backend event assignment (one contributor per event)

## A. Goal
Make each scheduled event belong to exactly one contributor:
- store responsibility on the event rows
- enforce ownership rules

## A.1 Status (implementation + verification)

Legend:
- **Implemented** = code exists in the repo
- **Verified** = you have tested it manually (curl/Swagger)

| Item | Implemented | Verified |
| --- | --- | --- |
| `ResponsibleUserId` exists on `EventEntity` + DB migration exists | Yes | Yes |
| Startup backfill assigns missing `ResponsibleUserId` to seeded admin | Yes | Yes |
| `/db/event` write endpoints require login | Yes | Yes |
| Create event sets `ResponsibleUserId` to current user (admin can override via `responsibleUserId`) | Yes | Yes |
| Contributor can only reschedule/delete events they own | Yes | Yes |
| Admin can reschedule/delete any event | Yes | Yes |
| While `mustChangePassword=true`, `/db/event` write endpoints return 403 | Yes | Yes |

## B. Files we will touch (planned)
- `API/Models/EventEntity.cs`
- `API/Actions/EventActionsDb.cs`
- `API/Endpoints/EventDbEndpoints.cs`
- `API/Actions/ScheduleProjectionDb.cs` (only if we want to surface ownership in the read model)

### B.1 Related plan documents
 - `../01-requirements.md`
   - Confirms the rule “one contributor per event”.
 - `../02-auth-roles-security.md`
   - Defines the role rules (contributors can’t manage other users; admins can manage everything).
 - `../03-database-schema.md`
   - Specifies adding `ResponsibleUserId` to the `Events` table.
 - `../04-api-contract.md`
   - Describes how `/db/event/*` endpoints will be secured and how ownership rules apply.
 - `../06-payment-calculation.md`
   - Payments use events filtered by responsible user, so ownership fields must be correct.
 - `../07-decision-log.md`
   - Records the rule that existing pre-Identity events are backfilled to the seeded admin and that `ResponsibleUserId` is intended to be NOT NULL.
 - `../08-codebase-map-and-gotchas.md`
   - Practical notes for where to implement ownership checks and what existing behavior must remain public.
 - `../09-definition-of-done-test-checklist.md`
   - Verification checklist for ownership enforcement and legacy event backfill.

## C. Step-by-step
### C.1 Add responsible user id to the database
Add to `EventEntity`:
- `public string ResponsibleUserId { get; set; }`

Create migration and update DB.

Decision note:
- Existing pre-Identity events must be backfilled to the seeded admin (`tobias@test.se`).
- Long-term, `ResponsibleUserId` is intended to be **NOT NULL**.
- If needed for SQLite safety, do it in two migrations: nullable -> backfill -> enforce NOT NULL.

### C.2 Secure event write endpoints
Existing endpoints:
- `POST /db/event/post`
- `POST /db/event/{id}/reschedule`
- `POST /db/event/{id}/delete`

Plan:
- Make them require authentication.
- Read-only schedule endpoints remain public.

### C.3 Set owner on create
When creating an event:
- default `ResponsibleUserId = current logged-in user id`

(Admin-only option)
- admin can create events for another user if needed (either a query param or separate assign endpoint).

### C.4 Enforce ownership on edit/delete
Rules:
- Contributor:
  - can only edit/delete events where `ResponsibleUserId == currentUserId`
- Admin:
  - can edit/delete any event

Implementation idea:
- Load the event entity by id
- Compare `ResponsibleUserId`
- Check role `Admin`

### C.5 Consider schedule projection
The public schedule does not need to show contributor ids.
So we can keep read models the same for now.

## D. Verification checklist
- Contributor creates event => event row has their `ResponsibleUserId`
- Contributor cannot delete another contributor’s event (403)
- Admin can delete any event
