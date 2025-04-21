# scraper/zen_scraper.py

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
URL = "https://www.zen.com.tn/fr/tn/175-nouveautes-enfant"
OUTPUT_DIR = os.path.join(os.path.dirname(__file__), "..", "data")
os.makedirs(OUTPUT_DIR, exist_ok=True)
CSV_FILE_PATH = os.path.join(OUTPUT_DIR, "zen_products.csv")

# --------------------------
# Setup Selenium WebDriver
# --------------------------
options = Options()
options.add_argument("--headless")  # Run in headless mode (no browser window)
driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()), options=options)

# --------------------------
# Load and Parse the Web Page
# --------------------------
# driver.get(URL)
# time.sleep(5)  # Let JavaScript load content

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
products = soup.select("div.row > div")
print(f"[INFO] Found {len(products)} products.")

with open(CSV_FILE_PATH, mode="w", newline="", encoding="utf-8") as file:
    writer = csv.writer(file)
    writer.writerow(["Name", "Price", "Description", "Image URL", "Number of Colors", "Product Link"])

    for product in products:
        name_tag = product.select_one("div.content h3 a")
        name = name_tag.text.strip() if name_tag else "N/A"

        price_tag = product.select_one("strong.new-price")
        price = price_tag.text.strip() if price_tag else "N/A"

        description_tag = product.select_one("div.content p")
        description = description_tag.text.strip() if description_tag else "No description available"

        image_tag = product.select_one("div.single-products-box img")
        image_url = image_tag["src"] if image_tag and "src" in image_tag.attrs else "No image available"

        colors_tag = product.select_one("span.more-colors")
        number_of_colors = colors_tag.text.strip() if colors_tag else "N/A"

        link_tag = product.select_one("div.content h3 a")
        product_link = link_tag["href"] if link_tag and "href" in link_tag.attrs else "N/A"

        writer.writerow([name, price, description, image_url, number_of_colors, product_link])
        print(f"[SAVED] {name} - {price}")

print(f"[DONE] Data saved to: {CSV_FILE_PATH}")
