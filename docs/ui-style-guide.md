# Reading Tracker - UI Style Guide (RT-09)

Status: Proposed team baseline, shared on Trello for David and Wadzanai to review.
Owner of implementation: David (pages/layout); Wadzanai (status/filter components and responsive checks).
Do not change their Razor/CSS files without coordination.

## 1. Goal

A simple, readable book list that works on phones and computers. Prefer the
Bootstrap styles already installed in the starter app; avoid a custom framework.

## 2. Colors and contrast

| Purpose | Bootstrap class | Reference color |
| --- | --- | --- |
| Primary action/link | `btn-primary`, `text-primary` | Bootstrap blue #0d6efd |
| Success/Finished | `text-bg-success` | Bootstrap green #198754 |
| Want to Read | `text-bg-secondary` | Bootstrap gray #6c757d |
| Reading | `text-bg-primary` | Bootstrap blue #0d6efd |
| Error/delete | `btn-danger`, `text-danger` | Bootstrap red #dc3545 |
| Main body text | `text-body` | Bootstrap dark #212529 |
| Background | `bg-body` | White/light theme |

These are suggested component classes, **not a requirement to recolor the existing
navigation**. Do not use color alone to indicate status: always show the text
"Want to Read", "Reading" or "Finished". Test actual foreground/background
contrast before approval; aim for WCAG AA (4.5:1 for ordinary text).

## 3. Typography and spacing

- Use the existing system/Helvetica/Arial font stack from `wwwroot/app.css`;
  do not add external fonts.
- One `h1` per page, followed by `h2` as needed.
- Body text at least 16px; avoid tiny badges and placeholders replacing labels.
- Use Bootstrap spacing utilities: `mb-3` between form groups, `gap-2` for
  actions, `p-3` for book cards and `mb-4` between sections.
- Keep main reading content roughly 960px wide or less when practical.
- Use a simple `container` and `row g-3` pattern instead of fixed widths.

## 4. Buttons, forms and cards

- **Add Book / Save:** `btn btn-primary`.
- **Cancel / Back:** `btn btn-outline-secondary`.
- **Edit:** `btn btn-outline-primary`.
- **Delete:** `btn btn-outline-danger`, followed by an explicit confirmation.
- Inputs use `form-label` with a visible label and `form-control` /
  `form-select` as appropriate.
- Validation errors appear next to their fields and in a short form summary;
  show loading/success/error states, not only a color change.
- A book card uses `card h-100`, `card-body`, title heading, author,
  visible reading status and Edit/Delete actions.
- Use semantic `button` elements for actions, `a` / `NavLink` for navigation.
- For unknown or unauthorized book IDs, show a clear message rather than
  displaying or editing another user's book.

## 5. Reading-status display and filter

| Model enum | Display text | Suggested badge |
| --- | --- | --- |
| `WantToRead` | Want to Read | `badge text-bg-secondary` |
| `Reading` | Reading | `badge text-bg-primary` |
| `Finished` | Finished | `badge text-bg-success` |

The filter offers **All Books, Want to Read, Reading, Finished**.
"All Books" is a UI-only filter option; it is **not** a ReadingStatus enum value.
Wadzanai's components should communicate via parameters and callbacks; David's
My Books page consumes them. Do not change `Book.UserId` in any form.

## 6. Phone and accessibility rules

- Build for ~360px phone width first; controls must not overflow horizontally.
- On phones, stack the filter and Add Book button and show one book card per row.
- On wider screens, use a responsive Bootstrap grid (e.g. col-12 col-md-6).
- Make click/touch targets comfortably sized; aim for at least ~44x44 CSS pixels.
- Preserve visible keyboard focus and a logical tab order.
- Ensure input labels, accessible button names and status text are present.
- Show distinct empty and error states. Never require the user to interpret
  a color or icon alone.

## 7. Review checklist

- [ ] David confirms page layout, buttons and form patterns.
- [ ] Wadzanai confirms badge/filter design and mobile/accessibility checks.
- [ ] Steven confirms no page bypasses IBookService or trusts submitted UserId.
- [ ] Test in browser at phone width, desktop width and with keyboard.
- [ ] Update the guide after team feedback if choices change.

Related page sketches: [ui-planning.md](ui-planning.md).
