package main

import (
	"fmt"
	"net/http"

	"github.com/gorilla/mux"
)

func main(){
	router := mux.NewRouter()

	// Définir les routes
	router.HandleFunc("/products", HelloHandler).Methods("GET")

	// Démarrer le serveur
	fmt.Println("Server is running on http://localhost:8080")
	http.ListenAndServe(":8080", router)
}

// Handlers
func HelloHandler(w http.ResponseWriter, r *http.Request) {
	fmt.Fprint(w, "Hello CESI ! called by product")
}
