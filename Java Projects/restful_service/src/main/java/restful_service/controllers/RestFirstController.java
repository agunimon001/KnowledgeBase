package restful_service.controllers;

import java.util.ArrayList;
import java.util.List;

import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RestController;

/**
 * This class contains some of the available ways to use @RestController.
 * 
 * @author Hock Leng
 */

// Additional parameter can be inserted for control. In this case, this REST
// controller is mapped to "/" and all other mapped methods will follow after.
@RestController("/")
public class RestFirstController {
	
	// @RequestMapping can be used universally for all RequestMethod
	@RequestMapping(method = RequestMethod.GET, value = "")
	public String first() {
		return "Hello";
	}

	// @RequestMapping for GET
	@GetMapping("second")
	public String second() {
		return "second";
	}

	// Example of returning an anonymous object
	// Note that getters are required for producing JSON output
	@GetMapping("third")
	public Object third() {
		return new Object() {
			String name = "Alpha";
			int age = 5;

			public String getName() {
				return name;
			}

			public int getAge() {
				return age;
			}
		};
	}

	// Returns a Person object
	@GetMapping("fourth")
	public Person fourth() {
		return new Person("Jack", 14);
	}

	// Inner class used for "fourth"
	class Person {
		String name;
		int age;

		Person(String name, int age) {
			this.name = name;
			this.age = age;
		}

		public String getName() {
			return name;
		}

		public int getAge() {
			return age;
		}

	}

	// Serves a list of Persons
	@GetMapping("fifth")
	public List<Person> fifth() {
		return new ArrayList<Person>() {
			{
				add(new Person("Alpha", 15));
				add(new Person("Bravo", 14));
			}
		};
	}

	// Serves a list of lists of Persons
	@GetMapping("sixth")
	public List<List<Person>> sixth() {
		List<Person> first = new ArrayList<Person>();
		List<Person> second = new ArrayList<Person>();

		first.add(new Person("Alpha", 14));
		first.add(new Person("Bravo", 17));

		second.add(new Person("Charlie", 6));
		second.add(new Person("Delta", 8));

		return new ArrayList<List<Person>>() {
			{
				add(first);
				add(second);
			}
		};
	}

}
