/*
 * Circle.cs
 * RVO2 Library C#
 *
 * Copyright 2008 University of North Carolina at Chapel Hill
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Please send all bug reports to <geom@cs.unc.edu>.
 *
 * The authors may be contacted via:
 *
 * Jur van den Berg, Stephen J. Guy, Jamie Snape, Ming C. Lin, Dinesh Manocha
 * Dept. of Computer Science
 * 201 S. Columbia St.
 * Frederick P. Brooks, Jr. Computer Science Bldg.
 * Chapel Hill, N.C. 27599-3175
 * United States of America
 *
 * <http://gamma.cs.unc.edu/RVO2/>
 */

/*
 * Example file showing a demo with 250 agents initially positioned evenly
 * distributed on a circle attempting to move to the antipodal position on the
 * circle.
 */

#define RVO_OUTPUT_TIME_AND_POSITIONS

using System;
using System.Collections.Generic;
using Lockstep.Math;

namespace RVO
{
    class Circle
    {
        /* Store the goals of the agents. */
        IList<LFloat2> goals;

        Circle()
        {
            goals = new List<LFloat2>();
        }

        void setupScenario()
        {
            /* Specify the global time step of the simulation. */
            Simulator.Instance.setTimeStep(new LFloat(true, 250));

            /*
             * Specify the default parameters for agents that are subsequently
             * added.
             */
            Simulator.Instance.setAgentDefaults(new LFloat(15), 10, new LFloat(10), new LFloat(10), new LFloat(true, 1500), new LFloat(2), LFloat2.zero);

            /*
             * Add agents, specifying their start position, and store their
             * goals on the opposite side of the environment.
             */
            for (int i = 0; i < 250; ++i)
            {
                Simulator.Instance.addAgent(200 *
                    new LFloat2(true, (float)Math.Cos(i * 2.0f * Math.PI / 250.0f),
                        (float)Math.Sin(i * 2.0f * Math.PI / 250.0f)));
                goals.Add(-Simulator.Instance.getAgentPosition(i));
            }
        }

#if RVO_OUTPUT_TIME_AND_POSITIONS
        void updateVisualization()
        {
            /* Output the current global time. */
            Console.Write(Simulator.Instance.getGlobalTime());

            /* Output the current position of all the agents. */
            for (int i = 0; i < Simulator.Instance.getNumAgents(); ++i)
            {
                Console.Write(" {0}", Simulator.Instance.getAgentPosition(i));
            }

            Console.WriteLine();
        }
#endif

        void setPreferredVelocities()
        {
            /*
             * Set the preferred velocity to be a vector of unit magnitude
             * (speed) in the direction of the goal.
             */
            for (int i = 0; i < Simulator.Instance.getNumAgents(); ++i)
            {
                LFloat2 goalVector = goals[i] - Simulator.Instance.getAgentPosition(i);

                if (RVOMath.absSq(goalVector) > LFloat.one)
                {
                    goalVector = RVOMath.normalize(goalVector);
                }

                Simulator.Instance.setAgentPrefVelocity(i, goalVector);
            }
        }

        bool reachedGoal()
        {
            /* Check if all agents have reached their goals. */
            for (int i = 0; i < Simulator.Instance.getNumAgents(); ++i)
            {
                if (RVOMath.absSq(Simulator.Instance.getAgentPosition(i) - goals[i]) > Simulator.Instance.getAgentRadius(i) * Simulator.Instance.getAgentRadius(i))
                {
                    return false;
                }
            }
            return true;
        }

        public static void Main(string[] args)
        {
            Circle circle = new Circle();

            /* Set up the scenario. */
            circle.setupScenario();

            /* Perform (and manipulate) the simulation. */
            do
            {
#if RVO_OUTPUT_TIME_AND_POSITIONS
                circle.updateVisualization();
#endif
                circle.setPreferredVelocities();
                Simulator.Instance.doStep();
            }
            while (!circle.reachedGoal());
        }
    }
}
