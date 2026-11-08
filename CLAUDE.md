# מתכון (RecipeVault)

מערכת לניהול מתכונים: מעלים מתכון מכל סוג קובץ, והמערכת מחלצת ממנו מתכון מובנה שאפשר לחפש בו ולבשל ממנו. זה פרויקט תיק עבודות לראיונות, ולכן איכות הקוד והתיעוד חשובות כמו הפיצ'רים.

האפיון המלא נמצא ב־[docs/spec.md](docs/spec.md). כשיש סתירה בין הקובץ הזה לאפיון, האפיון קובע, וצריך לעדכן את הקובץ הזה.

## תקשורת

- מדברים איתי בעברית: הסברים, שאלות וסיכומים.
- שמות קוד, פקודות ומונחים טכניים נשארים באנגלית.
- לפני כל פקודה שמשנה משהו מחוץ לתיקיית הפרויקט (התקנה גלובלית, הגדרות מערכת, קבצים בתיקיות אחרות) מבקשים אישור.
- את `README.md` לא משנים בלי בקשה מפורשת.
- כשאני מבקשת לסכם, מוסיפים שורה ל־[docs/log.md](docs/log.md).

## סטאק

| שכבה | טכנולוגיה |
|---|---|
| לקוח | React, TypeScript, Vite, Tailwind CSS |
| שרת | ASP.NET Core Web API (C#) |
| גישה לנתונים | Entity Framework Core |
| מסד נתונים | PostgreSQL |
| ניהול מצב שרת | TanStack Query |
| אימות | ASP.NET Core Identity |
| עיבוד ברקע | Hangfire |
| לוגים | Serilog |
| בדיקות | xUnit, Testcontainers, Vitest, React Testing Library, Playwright |

## מבנה תיקיות

```
api/
  RecipeVault.sln
  src/
    RecipeVault.Api/             Web API, controllers, DI
    RecipeVault.Core/            business logic, no cloud packages
    RecipeVault.Infrastructure/  EF Core, file storage, queue, extraction
  tests/
    RecipeVault.Core.Tests/
    RecipeVault.Api.IntegrationTests/
  Dockerfile
client/                          React + TypeScript + Vite
  Dockerfile
docs/                            spec, work log, documentation
  design-reference/              design reference screenshots
docker-compose.yml               runs the whole environment from the root
```

- ה־Dockerfile של כל אפליקציה נמצא בתיקייה שלה, ו־`docker-compose.yml` נמצא בשורש.
- `RecipeVault.Core` לא מפנה ל־Infrastructure ולא לחבילות ענן. כיוון התלויות: Api → Infrastructure → Core.
- תיקיית `infra/` (Bicep) תתווסף רק בהרחבה ל־Azure.

## עיצוב

### צבעים

| תפקיד | ערך | שימוש |
|---|---|---|
| רקע העמוד | `#F7F8F2` | קרם בהיר עם גוון ירקרק |
| משטחי משנה | `#ECEEE3` | באנר ראשי, פאנל "מה צריך?", דף המחברת |
| צבע ראשי | `#9D3D31` | כפתורי פעולה, פריט פעיל בתפריט, תיבת סימון פעילה |
| טקסט וכותרות | `#2C2F26` | זית כהה כמעט שחור, וגם הצ'יפ הפעיל |
| דגש זית | `#536744` | תגיות, שורת הכותרת המשנית, מצב בישול |
| כרטיס טיפ והתראה | `#F2ECE0` | "קטנה מהמטבח" והודעות מידע |

- כרטיסים ושדות לבנים.
- מגדירים את הצבעים פעם אחת, כטוקנים בהגדרות של Tailwind, ומשתמשים רק בשמות שלהם. לא כותבים ערכי hex בתוך רכיבים.
- ניגודיות ברמת WCAG AA לפחות.

### טיפוגרפיה

- הגופן הוא **Heebo** לכל הממשק.
- כותרות במשקל כבד, וטקסט משני באפור.

### סגנון

- פינות מעוגלות (16 עד 24px), בלי גרדיאנטים ובלי אימוג'י.

### רספונסיביות

- נקודות שבירה: עד 639px טלפון, 640–1023px טאבלט, 1024px ומעלה מחשב, 1440px ומעלה מסך רחב (רוחב תוכן מרבי כ־1200px).

## RTL ועברית

- `<html lang="he" dir="rtl">`. כל הממשק בעברית ובכיוון מימין לשמאל מהיום הראשון.
- **משתמשים רק במאפייני CSS לוגיים**, לא ב־left ו־right:
  - `margin-inline-start` / `margin-inline-end` במקום `margin-left` / `margin-right`
  - `padding-inline-start` / `padding-inline-end`
  - `inset-inline-start` / `inset-inline-end` במקום `left` / `right`
  - `border-inline-start` / `border-inline-end`
  - `text-align: start` / `end` במקום `left` / `right`
- ב־Tailwind משתמשים במחלקות הלוגיות: `ms-*`, `me-*`, `ps-*`, `pe-*`, `start-*`, `end-*`, `text-start`, `text-end`, `rounded-s-*`, `rounded-e-*`, `border-s`, `border-e`. לא משתמשים ב־`ml-*`, `mr-*`, `pl-*`, `pr-*`, `left-*`, `right-*`.
- אייקונים עם כיוון (חצים, "הבא", "הקודם") מתהפכים ב־RTL. אייקונים בלי כיוון לא מתהפכים.
- תוכן באנגלית או מספרים בתוך טקסט עברי (כמויות, קוד, כתובות) עוטפים ב־`dir="ltr"` או ב־`<bdi>` כשצריך.
- תפריט הצד נמצא בצד ימין, כלומר inline-start.

## כללי תחום

- מתכון שיובא מקובץ תמיד עובר אישור של המשתמש לפני שהוא נשמר כמאושר. מתכון שנכתב ידנית נשמר ישר כמאושר.
- RawText של מרכיב נשמר תמיד כפי שנכתב במקור.
- בונים רק פיצ'רים של ה־MVP. מצב בישול, התאמת כמויות, רשימת קניות וחיפוש לפי מה שיש בבית שייכים לשלב 2, ולא בונים אותם עכשיו.

## כללי קוד

### כללי

- קוד, שמות משתנים, קבצים והערות בקוד באנגלית. רק הטקסט שמוצג למשתמש בעברית.
- פונקציות קטנות עם אחריות אחת. לא מוסיפים פיצ'רים שלא הוגדרו באפיון.
- אין סודות בקוד: מחרוזות חיבור, מפתחות API וסיסמאות נכנסים למשתני סביבה, ו־`.env` לא נכנס ל־git.
- כל לוגיקה עסקית חדשה מגיעה עם בדיקות.
- הודעות קומיט באנגלית, בפורמט Conventional Commits (`feat:`, `fix:`, `docs:`, `chore:` וכדומה).

### צד הלקוח (React + TypeScript)

- TypeScript במצב `strict`, ובלי `any`.
- רכיבים פונקציונליים בלבד, ושם הקובץ בתבנית PascalCase (`RecipeCard.tsx`).
- כתובת הבסיס של ה־API נקבעת במשתנה סביבה (`VITE_API_BASE_URL`).
- כל מסך מטפל בשלושה מצבים: ריק, טעינה ושגיאה.
- נגישות: לכל שדה יש תווית, אפשר לנווט במקלדת, ויעדי מגע הם בגודל 44px לפחות.
- רספונסיבי מ־320px, בלי גלילה אופקית.

### צד השרת (C# + EF Core)

- מוסכמות שמות של .NET: PascalCase לטיפוסים ולמתודות, ו־camelCase למשתנים מקומיים.
- Nullable reference types מופעלים.
- `async`/`await` לכל פעולת קלט/פלט, ולא משתמשים ב־`.Result` או ב־`.Wait()`.
- כל שאילתה על מתכונים וקבצים מסוננת לפי המשתמש המחובר.
- שירותים חיצוניים (אחסון קבצים, חילוץ, תור) מוסתרים מאחורי ממשק ונרשמים ב־Dependency Injection.
- הלוגיקה העסקית לא מפנה לחבילות של שירותי ענן.
- שינויים במסד הנתונים נעשים רק דרך מיגרציות של EF Core.
