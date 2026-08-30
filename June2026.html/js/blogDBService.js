class BlogDBService {
  constructor(_dbName = "BlogDB", _storeName = "blogs") {
    this.dbName = _dbName;
    this.storeName = _storeName;
    this.db = null;
  }
  //Open(or create) the database
  async openDB() {
    if (this.db) return this.db;
    try {
      const db = await new Promise((resolve, reject) => {
        const request = indexedDB.open(this.dbName, 1);
        request.onupgradeneeded = (event) => {
          const db = event.target.result;
          if (!db.objectStoreNames.contains(this.storeName)) {
            const store = db.createObjectStore(this.storeName, {
              keyPath: "id",
            });
            store.createIndex("title", "title", { unique: false });
            store.createIndex("author", "author", { unique: false });
        
          }
        };
        request.onsuccess = (event) => resolve(event.target.result);
        request.onerror = (event) => reject(event.target.error);
      });
      this.db = db;
      return this.db;
    } catch (error) {
      console.error("Failed to open IndexDB", error);
      throw error;
    }
  }
  //ensure database is ready to open before operation
  async ensureDB() {
    if (!this.db) await this.openDB();
    return this.db;
  }
  //Get all records
  async getAllBlogs() {
    await this.ensureDB();
    try {
      const result = await new Promise((resolve, reject) => {
        const tx = this.db.transaction([this.storeName], "readonly");
        const store = tx.objectStore(this.storeName);
        const request = store.getAll();
        request.onsuccess = () => resolve(request.result || []);
        request.onerror = () => reject(request.error);
      });
      return result;
    } catch (error) {
      console.error("getAll failed", error);
      throw error;
    }
  }
  //Get single record by id
  async getBlogById(id) {
    await this.ensureDB();
    try {
      const result = await new Promise((resolve, reject) => {
        const tx = this.db.transaction([this.storeName], "readonly");
        const store = tx.objectStore(this.storeName);
        const request = store.get(id);
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
      });
      return result;
    } catch (error) {
      console.error(`get (${id}) failed`, error);
      throw error;
    }
  }
  //create new blog
  async createBlog(data) {
    await this.ensureDB();
    try {
      const result = await new Promise((resolve, reject) => {
        const tx = this.db.transaction([this.storeName], "readwrite");
        const store = tx.objectStore(this.storeName);
        const request = store.add(data);
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
      });
      return result;
    } catch (error) {
      console.error("Failed to add data", error);
      throw error;
    }
  }
  //update existing blog
  async updateBlog(data) {
    await this.ensureDB();
    try {
      const result = await new Promise((resolve, reject) => {
        const tx = this.db.transaction([this.storeName], "readwrite");
        const store = tx.objectStore(this.storeName);
        const request = store.put(data);
        request.onsuccess = () => resolve(request.result);
        request.onerror = () => reject(request.error);
      });
      return result;
    } catch (error) {
      console.error("update failed", error);
      throw error;
    }
  }
  //delete blog by id
  async deleteBlog(id) {
    await this.ensureDB();
    try {
      await new Promise((resolve, reject) => {
        const tx = this.db.transaction([this.storeName], "readwrite");
        const store = tx.objectStore(this.storeName);
        const request = store.delete(id);
        request.onsuccess = () => resolve();
        request.onerror = () => reject(request.error);
      });
    } catch (error) {
      console.error(`delete(${id}) failed:`, error);
      throw error;
    }
  }
}
