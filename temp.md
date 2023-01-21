## StrangeCSharpTricks v1

#Attribute
## GET /Attribute/{name}
 #### Input Parameters
       name [stringNullable] [Required] From Path
 #### Success Output responses
 [200] Success
 #### Response Properties
            name [string]
            type [integer]
            isRequired [boolean]
            min [integer]
            max [integer]
            minLength [integer]
            maxLength [integer]
 #### Bad Output responses
       [404] Not Found
## DELETE /Attribute/{name}
 #### Input Parameters
       name string [Nullable] [Required] From Path
 #### Success Output responses
 [204] Success
 #### Bad Output responses
       [404] Not Found
#Attribute
## GET /Attribute
 #### Input Parameters
 #### Success Output responses
 [200] Success
 Response Properties
  
  An array of:

       name [string]
       type [integer]
       isRequired [boolean]
       min [integer]
       max [integer]
       minLength [integer]
       maxLength [integer]
 #### Bad Output responses
## POST /Attribute
 #### Input Parameters
 Request Body Properties
       name [string]
       type [integer]
       isRequired [boolean]
       min [integer]
       max [integer]
       minLength [integer]
       maxLength [integer]
 #### Success Output responses
 [201] Success
 Response Properties
       name [string]
       type [integer]
       isRequired [boolean]
       min [integer]
       max [integer]
       minLength [integer]
       maxLength [integer]
 #### Bad Output responses
       [400] Bad Request
## PUT /Attribute
 #### Input Parameters
 Request Body Properties
       name [string]
       type [integer]
       isRequired [boolean]
       min [integer]
       max [integer]
       minLength [integer]
       maxLength [integer]
 #### Success Output responses
 [200] Success
 Response Properties
       name [string]
       type [integer]
       isRequired [boolean]
       min [integer]
       max [integer]
       minLength [integer]
       maxLength [integer]
 #### Bad Output responses
       [404] Not Found
#Docs
## GET /Docs
 #### Input Parameters
       path [stringNullable]From Query
 #### Success Output responses
 [200] Success
 #### Bad Output responses
#Entity
## POST /Entity
 #### Input Parameters
 Request Body Properties
 #### Success Output responses
 [201] Success
 Response Properties
 #### Bad Output responses
       [400] Bad Request