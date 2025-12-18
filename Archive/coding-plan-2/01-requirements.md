# Requirements – Radio station contributors

## 1. Functional requirements (from the assignment)
- **1.1 Admin creates contributors**
  - Contributors are created by an authenticated Admin.
- **1.2 Contributor personal data**
  - Address
  - Phone
  - Email
  - Payment history
  - Optional: promotional material (photo + short bio)
- **1.3 Payments**
  - Monthly payout based on the **previous month**.
  - Flat pricing:
    - 750 SEK/hour
    - +300 SEK per scheduled event
  - VAT:
    - 25% VAT calculated on the monthly total
- **1.4 Authentication**
  - Each contributor must have an `IdentityUser` attached.
  - Contributors authenticate by login.
- **1.5 Protected admin endpoints**
  - Managing contributors must be protected endpoints (login required).
  - Only authorized personnel can access admin endpoints.
- **1.6 Contributor page (frontend minimum)**
  - Frontend can be less developed, but must include:
    - A personal contributor page showing current data.

## 2. Non-functional requirements (quality)
- **2.1 Security**
  - No public self-registration.
  - Contributors must not be able to obtain Admin rights.
  - Only Admin can create/delete users.
  - Temporary password must be changed on first login.
- **2.2 Correctness**
  - Payment calculation must match examples:
    - 1 hour event => 750 + 300 = 1050 SEK (ex VAT)
    - 2 hour event => 1500 + 300 = 1800 SEK (ex VAT)
  - VAT = 25% of monthly subtotal.
- **2.3 Data persistence**
  - All users, events, and payment history must be stored in the database (`API/scheduler.db`).

## 3. MVP scope (what we will implement first)
- **3.1 Backend**
  - Identity login (cookie-based)
  - Roles: Admin/Contributor
  - Admin user management endpoints
  - Contributor "me" endpoint
  - Payment calculation + history table
  - One contributor per event: add `ResponsibleUserId` to event records
- **3.2 Frontend**
  - Login page
  - Change password page (first login)
  - My Contributor page (personal data + payment history)

## 4. Optional enhancements (nice-to-have)
- **4.1 Promotional material**
  - Upload or store a photo URL
  - Store a short bio summary
- **4.2 Admin UI**
  - Full CRUD UI for users (table, filters)
- **4.3 Payments UI**
  - Admin: "Calculate last month" button
  - Contributor: detailed invoice-like view
