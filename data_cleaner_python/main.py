import pandas as pd
import os

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
    if "femme" in file:
        df["category"] = "femme"
    elif "homme" in file:
        df["category"] = "homme"
    elif "enfant" in file:
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

    # Sauvegarder fichier intermédiaire
    df.to_csv(file_path, index=False, encoding="utf-8")
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