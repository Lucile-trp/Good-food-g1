package controller

import (
	"net/http"
	"product/internal/entity"
	"product/internal/repo"
	"product/pkg/logger"
	"strconv"

	"github.com/gin-gonic/gin"
)

type productRoutes struct {
	l logger.Interface
	p repo.ProductRepo
}

type errorMessage struct {
	Message string `json:"error"`
}

func NewRouter(handler *gin.Engine, l logger.Interface, p repo.ProductRepo) {
	handler.Use(gin.Logger())
	handler.Use(gin.Recovery())

	r := productRoutes{l, p}

	// Routers
	h := handler.Group("/api/v1")
	{
		h.GET("/dishes", r.GetDishes)
		h.GET("/dish", r.GetDish)
		h.POST("/dish", r.CreateDish)
		h.GET("/restaurants", r.GetRestaurants)
		h.GET("/restaurant", r.GetRestaurant)
		h.POST("/restaurant", r.CreateRestaurant)
		h.GET("/images", r.GetImages)
		h.GET("/image", r.GetImage)
		h.POST("/image", r.CreateImage)
	}
}

func (r productRoutes) GetDishes(c *gin.Context) {
	dishes, err := r.p.GetDishes(c.Request.Context())
	if err != nil {
		r.l.Error(err, "GetDishes on  get dishes")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, dishes)
}

func (r productRoutes) GetDish(c *gin.Context) {
	id, err := strconv.Atoi(c.Query("id"))
	if err != nil {
		r.l.Error(err, "GetDish on  get dish")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Query errors"})

		return
	}

	dish, err := r.p.GetDish(c.Request.Context(), id)
	if err != nil {
		r.l.Error(err, "GetDish on  get dish")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, dish)
}

func (r productRoutes) CreateDish(c *gin.Context) {
	var dish entity.Dish

	err := c.BindJSON(&dish)

	if err != nil {
		r.l.Error(err, "BindJSON on insert dish")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Conversion errors"})

		return
	}

	err = r.p.InsertDish(c.Request.Context(), dish)

	if err != nil {
		r.l.Error(err, "InsertDish on insert dish")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Database erros"})

		return
	}
}

func (r productRoutes) GetRestaurants(c *gin.Context) {
	restaurants, err := r.p.GetRestaurants(c.Request.Context())
	if err != nil {
		r.l.Error(err, "GetRestaurants on  get restaurants")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, restaurants)
}

func (r productRoutes) GetRestaurant(c *gin.Context) {
	id, err := strconv.Atoi(c.Query("id"))
	if err != nil {
		r.l.Error(err, "GetRestaurant on  get restaurant")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Query errors"})

		return
	}

	restaurant, err := r.p.GetRestaurant(c.Request.Context(), id)
	if err != nil {
		r.l.Error(err, "GetRestaurant on  get restaurant")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, restaurant)
}

func (r productRoutes) CreateRestaurant(c *gin.Context) {
	var restaurant entity.Restaurant

	err := c.BindJSON(&restaurant)

	if err != nil {
		r.l.Error(err, "BindJSON on insert restaurant")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Conversion errors"})

		return
	}

	err = r.p.InsertRestaurant(c.Request.Context(), restaurant)

	if err != nil {
		r.l.Error(err, "InsertRestaurant on insert restaurant")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Database erros"})

		return
	}
}

func (r productRoutes) GetImages(c *gin.Context) {
	images, err := r.p.GetImages(c.Request.Context())
	if err != nil {
		r.l.Error(err, "GetImages on  get images")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, images)
}

func (r productRoutes) GetImage(c *gin.Context) {
	id, err := strconv.Atoi(c.Query("id"))
	if err != nil {
		r.l.Error(err, "GetImage on  get image")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Query errors"})

		return
	}

	image, err := r.p.GetImage(c.Request.Context(), id)
	if err != nil {
		r.l.Error(err, "GetImage on  get image")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "DataBase errors"})

		return
	}

	c.JSON(http.StatusOK, image)
}

func (r productRoutes) CreateImage(c *gin.Context) {
	var image entity.Image

	err := c.BindJSON(&image)

	if err != nil {
		r.l.Error(err, "BindJSON on insert image")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Conversion errors"})

		return
	}

	err = r.p.InsertImage(c.Request.Context(), image)

	if err != nil {
		r.l.Error(err, "InsertImage on insert image")
		c.AbortWithStatusJSON(http.StatusInternalServerError, errorMessage{Message: "Database erros"})

		return
	}
}
