# controller :
start with controller. designing logic routing and responseing in controller. if u have error throw exceptions.
u can define exception class for errors and handle them in middlewares. (global error handling)
# middleware
working before controller and after controller.
u make security validations (validate users auth token etc.) on here before run the controller
u can redesign your reponse (processes like: merge somethings in your response before send to client)
# services:
process logic
# repo (repositories files)
db query logics.
# DTO:
object for transfering data format. (DTA => Data Tranfer Object)