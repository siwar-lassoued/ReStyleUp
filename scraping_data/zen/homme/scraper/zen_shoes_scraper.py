from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.chrome.options import Options
from bs4 import BeautifulSoup
import csv
import time
import os

# --------------------------
# Configuration
# --------------------------
URL = "https://www.zen.com.tn/fr/tn/162-chaussures-homme"
OUTPUT_DIR = os.path.join(os.path.dirname(__file__), "..", "data")
os.makedirs(OUTPUT_DIR, exist_ok=True)
CSV_FILE_PATH = os.path.join(OUTPUT_DIR, "zen_shoes_homme.csv")

# --------------------------
# Setup Selenium WebDriver
# --------------------------
options = Options()
options.add_argument("--headless")
options.add_argument("--no-sandbox")
options.add_argument("--disable-dev-shm-usage")
driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()), options=options)

# --------------------------
# Load and Scroll Page
# --------------------------
driver.get(URL)
time.sleep(3)

scroll_pause_time = 2
last_height = driver.execute_script("return document.body.scrollHeight")

while True:
    driver.execute_script("window.scrollTo(0, document.body.scrollHeight);")
    time.sleep(scroll_pause_time)
    new_height = driver.execute_script("return document.body.scrollHeight")
    if new_height == last_height:
        break
    last_height = new_height

soup = BeautifulSoup(driver.page_source, "html.parser")
driver.quit()

# --------------------------
# Scrape Product Information
# --------------------------
products = soup.select("div.row.ng-star-inserted > div")
print(f"[INFO] Found {len(products)} products.")

with open(CSV_FILE_PATH, mode="w", newline="", encoding="utf-8") as file:
    writer = csv.writer(file)
    writer.writerow([
        "Name", "Price", "Old Price", "Promo", "Number of Colors",
        "Color Image URLs", "Main Image URL", "Product Link"
    ])

    for product in products:
        name_tag = product.select_one("div.content h3 a")
        name = name_tag.text.strip() if name_tag else "N/A"

        price_tag = product.select_one("strong.new-price")
        price = price_tag.text.strip() if price_tag else "N/A"

        old_price_tag = product.select_one("span.old-price")
        old_price = old_price_tag.text.strip() if old_price_tag else "N/A"

        promo_tag = product.select_one("span.sale")
        promo = promo_tag.text.strip() if promo_tag else "No"

        colors_tag = product.select_one("span.more-colors")
        number_of_colors = colors_tag.text.strip() if colors_tag else "N/A"

        color_imgs = product.select("ul.colos-button img")
        color_image_urls = ", ".join([img["src"] for img in color_imgs if "src" in img.attrs])

        main_img_tag = product.select_one("a img")
        main_image_url = main_img_tag["src"] if main_img_tag and "src" in main_img_tag.attrs else "N/A"

        link_tag = product.select_one("a")
        product_link = "https://www.zen.com.tn" + link_tag["href"] if link_tag and "href" in link_tag.attrs else "N/A"

        writer.writerow([
            name, price, old_price, promo, number_of_colors,
            color_image_urls, main_image_url, product_link
        ])
        print(f"[SAVED] {name} - {price}")

print(f"[DONE] Data saved to: {CSV_FILE_PATH}")
