# 🛒 Minimarket App

O aplicație consolă eficientă pentru gestionarea stocului și procesarea vânzărilor într-un magazin alimentar virtual.

## 📋 Descriere
Acest proiect are rolul de a automatiza gestiunea produselor, permițând utilizatorului să monitorizeze stocurile în timp real, să realizeze vânzări și să calculeze valoarea totală a inventarului.

---

## 🚀 Funcționalități Principale

### 1. ➕ Adăugare Produs
Sistemul permite înregistrarea de noi articole în baza de date prin colectarea următoarelor informații:
* **Numele produsului**
* **Categoria** (ex: aliment, băutură)
* **Prețul** unitar
* **Cantitatea** disponibilă în depozit

### 2. 📦 Afișare Produse
Vizualizarea rapidă a întregului inventar:
* Listarea completă a tuturor produselor.
* Detalii specifice pentru fiecare item: `Nume | Categorie | Preț | Stoc`.

### 3. 🔍 Căutare Produs
Funcție de filtrare pentru identificarea rapidă a bunurilor:
* Căutare după **nume**.
* Validarea existenței în listă.
* Afișarea automată a detaliilor sau a unui mesaj de eroare dacă produsul este inexistent.

### 4. 💰 Vânzare Produs
Modulul de tranzacționare care asigură integritatea stocului:
* **Verificare stoc:** Aplicația verifică dacă există suficiente unități înainte de a procesa vânzarea.
* **Actualizare automată:** Scăderea cantității vândute din stocul total.
* **Calcul total:** Afișarea sumei de plată pentru client.
* **Gestionare erori:** Mesaje de avertizare în cazul în care stocul este insuficient.

---

## 🛠️ Tehnologii Utilizate
* [Ex: C++ / Java / Python]
* Lucru cu structuri de date (liste/vectori)
* Algoritmi de căutare și validare

---

## 📖 Cum se folosește
1. **Rulează** aplicația.
2. **Alege** o opțiune din meniul principal (1-4).
3. **Introdu** datele solicitate și urmărește instrucțiunile din consolă.
