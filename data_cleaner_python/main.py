import pandas as pd
import os
import random
import re

# 📁 Dossier contenant les fichiers CSV
DATA_FOLDER = os.path.join(os.path.dirname(__file__), "data")
csv_files = [f for f in os.listdir(DATA_FOLDER) if f.endswith(".csv")]

# Liste pour stocker les DataFrames harmonisés
dataframes = []

# ------------------------------
# 🔁 Étape 1 & 2 : Ajouter colonne 'category' et harmoniser
# ------------------------------
for file in csv_files:
    file_path = os.path.join(DATA_FOLDER, file)
    df = pd.read_csv(file_path, encoding="utf-8")

    # Étape 1 : Ajouter la catégorie selon le nom du fichier
    file_lower = file.lower()
    if "femme" in file_lower:
        df["category"] = "femme"
    elif "homme" in file_lower:
        df["category"] = "homme"
    elif "enfant" in file_lower:
        df["category"] = "enfant"
    else:
        df["category"] = "unknown"

    # Étape 2 : Harmonisation des colonnes
    if "Main Image URL" in df.columns:
        df["Image URL"] = df["Main Image URL"]
    if "Image URL" not in df.columns:
        df["Image URL"] = None

    keep_cols = ["Name", "Price", "Image URL", "Number of Colors", "Product Link", "category"]
    for col in keep_cols:
        if col not in df.columns:
            df[col] = None
    df = df[keep_cols]

    # Debug
    print(f"[CHECK] {file} après harmonisation :")
    print(df[["Name", "category"]].head())
    print(f"Répartition des catégories :\n{df['category'].value_counts()}")

    # 👉 Ne pas sauvegarder dans le fichier ici
    dataframes.append(df)
    print(f"[✅] Terminé pour : {file}")

# ------------------------------
# 🔁 Étape 3 : Fusionner les fichiers
# ------------------------------
merged_df = pd.concat(dataframes, ignore_index=True)
print("[CHECK] Répartition catégories fusionnées :")
print(merged_df["category"].value_counts())

merged_file_path = os.path.join(DATA_FOLDER, "merged_data.csv")
merged_df.to_csv(merged_file_path, index=False, encoding="utf-8")
print(f"[✅ STEP 3] Fichier fusionné : '{merged_file_path}'")

# ------------------------------
# 🧹 Étape 4 : Nettoyage
# ------------------------------
merged_df = merged_df[merged_df["Name"].notna()]
merged_df = merged_df[merged_df["Name"].str.strip().str.upper() != "N/A"]
merged_df.drop_duplicates(subset=["Name", "Price"], inplace=True)

cleaned_file_path = os.path.join(DATA_FOLDER, "merged_data_cleaned.csv")
merged_df.to_csv(cleaned_file_path, index=False, encoding="utf-8")
print(f"[✅ STEP 4] Nettoyage terminé : '{cleaned_file_path}'")

# ------------------------------
# 🎯 Étape 5 : Ajout des colonnes finales
# ------------------------------
# Ajouter 'brand' : "Zen"
merged_df["brand"] = "Zen"

# Ajouter 'collection' (année aléatoire)
merged_df["collection"] = [random.choice([2021, 2022, 2023, 2024, 2025]) for _ in range(len(merged_df))]

# Ajouter 'saison' (aléatoire)
merged_df["saison"] = [random.choice(["été", "automne", "printemps", "hiver"]) for _ in range(len(merged_df))]

# Réorganiser les colonnes pour la version finale
final_columns = ["Name", "Price", "brand", "category", "collection", "saison"]
final_df = merged_df[final_columns]

# Sauvegarde du fichier final
final_file_path = os.path.join(DATA_FOLDER, "final_data.csv")
final_df.to_csv(final_file_path, index=False, encoding="utf-8")
print(f"[✅ STEP 5] Fichier final créé : '{final_file_path}'")


def clean_price(price):
    if pd.isna(price):
        return None
    # Supprimer 'TND' et garder uniquement le premier prix
    matches = re.findall(r"\d+(?:\.\d+)?", str(price))
    return float(matches[0]) if matches else None

final_df["Price"] = final_df["Price"].apply(clean_price)

# 🔽 Chemin personnalisé pour sauvegarde finale
final_save_path = r"C:\Users\neebr\OneDrive\Bureau\données_brands\data_used_clothes_zen.csv"
final_df.to_csv(final_save_path, index=False, encoding="utf-8-sig")

print(f"[✅ STEP 6] Colonne 'Price' nettoyée et fichier sauvegardé à : '{final_save_path}'")