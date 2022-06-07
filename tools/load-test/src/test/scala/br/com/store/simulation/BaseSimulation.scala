package br.com.store.simulation

import br.com.store.utils.{Config, Utils}
import io.gatling.core.Predef._

abstract class BaseSimulation extends Simulation {
  def scn: String = Utils.getProperty("LT_SCENARIO", "addNewCustomerConstant")

  def userCount: Int = Utils.getProperty("LT_USERS", "6").toInt

  def rampDuration: Int = Utils.getProperty("LT_RAMP_DURATION", "240").toInt

  def constantDuration: Int = Utils.getProperty("LT_CONSTANT_DURATION", "240").toInt

  def maxDuration: Int = Utils.getProperty("LT_MAX_DURATION", "2").toInt

  Config.uris.api_url = Utils.getProperty("LT_API_URL", "https://localhost:7198")

  before {
    println("================================================================================")
    println("Starting the load tests, using the following settings:")
    println(s"Execution")
    println(s" - Chosen Scenario: ${scn}")
    println(s" - Executing tests with ${userCount} users")
    println(s" - User Ramp in ${rampDuration} seconds")
    println(s" - Constant users per ${rampDuration} seconds")
    println(s" - Maximum duration: ${maxDuration} hour(s)")
    println(s"Settings")
    println(s" - API URL: ${Config.uris.api_url}")
    println("================================================================================")
  }

  after {
    println("Simulation finished")
  }
}
