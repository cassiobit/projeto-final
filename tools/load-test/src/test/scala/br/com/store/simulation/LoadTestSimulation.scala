package br.com.store.simulation

import br.com.store.http.Http
import io.gatling.core.Predef._
import scala.concurrent.duration._
import br.com.store.scenario.{
  AddNewCustomer
}

/** Simulação a ser executada.
  */
class LoadTestSimulation extends BaseSimulation {
  val scenarios = Map(
    "addNewCustomerConstant" -> List(
      AddNewCustomer.cenario.inject(
        constantUsersPerSec(userCount) during (constantDuration seconds),
      )
    ),
    "addNewCustomerRamp"-> List(
      AddNewCustomer.cenario.inject(
        rampUsers(userCount) during (rampDuration seconds),
      )
    )
  )

  setUp(scenarios(scn): _*)
    .protocols(Http.httpProtocol)
    .maxDuration(maxDuration hours)
}
