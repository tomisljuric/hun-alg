# 🇭🇺 Mađarski algoritam (Hungarian Algorithm)

Implementacija Mađarskog algoritma u programskom jeziku C# za rješavanje problema dodjele (Assignment Problem).

## 📖 O algoritmu

Mađarski algoritam je algoritam kombinatorne optimizacije koji rješava problem optimalne dodjele u polinomijalnom vremenu.
Algoritam je razvio i objavio 1955. godine Harold Kuhn, a temelji se na radovima mađarskih matematičara Denesa Koniga i Jenoa Egervaryja

---

# 🎯 Problem koji algoritam rješava

Mađarski algoritam koristi se za rješavanje problema dodjele:
- radnika poslovima,
- vozila rutama,
- studenata projektima,
- letova destinacijama,
- resursa zadacima.

Cilj algoritma je pronaći optimalnu dodjelu uz minimalni trošak ili maksimalnu dobit.

---

# 🧹 Primjer problema – dodjela poslova

Tri radnika trebaju obaviti tri različita zadatka:

| Radnik | Čišćenje kupaonice | Metenje podova | Pranje prozora |
|---|---|---|---|
| Ana | 8 € | 4 € | 7 € |
| Branko | 5 € | 2 € | 3 € |
| Danijel | 9 € | 4 € | 8 € |

Primjenom Mađarskog algoritma dobiva se minimalni trošak:

```text
15 €
```

Optimalna dodjela:
- Ana → čišćenje kupaonice
- Danijel → metenje podova
- Branko → pranje prozora

---

# ✈️ Primjer problema – planiranje putovanja

Prodavači iz:
- Madrida
- Berlina
- Istanbula

trebaju putovati u:
- Bukurešt
- Zagreb
- London

| Grad prodavača | Bukurešt | Zagreb | London |
|---|---|---|---|
| Madrid | 300 € | 200 € | 100 € |
| Berlin | 150 € | 100 € | 80 € |
| Istanbul | 115 € | 200 € | 350 € |

Minimalni trošak iznosi:

```text
315 €
```

Optimalna dodjela:
- Madrid → London
- Berlin → Zagreb
- Istanbul → Bukurešt

---

# ⚙️ Način rada algoritma

Algoritam radi nad matricom troškova.

## Koraci algoritma

### 1️⃣ Oduzimanje minimuma svakog retka

Od svakog retka oduzima se najmanji element.

---

### 2️⃣ Oduzimanje minimuma svakog stupca

Od svakog stupca oduzima se najmanji element.

---

### 3️⃣ Pokrivanje svih nula

Sve nule pokrivaju se minimalnim brojem horizontalnih i vertikalnih linija.

---

### 4️⃣ Pronalaženje optimalne dodjele

Kada je moguće pokriti sve nule s `n` linija, pronađena je optimalna dodjela.

---

# 💻 Implementacija

Projekt je implementiran u programskom jeziku:

- C#

Korištene su:
- matrice troškova,
- BFS pretraga,
- augmentacijske putanje,
- oznake čvorova,
- slack vrijednosti.

Glavne metode implementacije:
- `Inicijalizacija()`
- `Azuriranje()`
- `Dodaj_u_stablo()`
- `Povecanje()`
- `Madariziraj()`
- `Dodjela()`

---

# 🛠️ Tehnologije

- C#
- .NET
- Konzolna aplikacija

---

# 🎯 Cilj projekta

Cilj projekta je:
- razumjeti problem optimalne dodjele,
- implementirati Mađarski algoritam,
- primijeniti kombinatornu optimizaciju,
- koristiti algoritme grafova i optimizacije u stvarnim problemima.
