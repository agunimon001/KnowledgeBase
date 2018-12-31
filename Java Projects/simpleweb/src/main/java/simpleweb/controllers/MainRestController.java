package simpleweb.controllers;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RestController;

/**
 * This class demonstrates RESTful API by sending back a string upon a GET
 * request.
 * 
 * @author Hock Leng
 *
 */

// @RestController is used to identify the class as a RESTful API controller.
@RestController
public class MainRestController {

	// @GetMapping is used to identify the method for GET request. "/" identifies the path.
	@GetMapping("/")
	public String home() {
		return "Hello World";
	}

}
