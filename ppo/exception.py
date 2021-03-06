#coding=utf-8

__author__ = 'penghuailiang'

'''
exception handler
'''


import logging
logger = logging.getLogger("bird")

class UnityEnvironmentException(Exception):
    """
    Related to errors starting and closing environment.
    """
    pass


class UnityActionException(Exception):
    """
    Related to errors with sending actions.
    """
    pass

class UnityTimeOutException(Exception):
    """
    Related to errors with communication timeouts.
    """
    def __init__(self, message, log_file_path = None):
        if log_file_path is not None:
            try:
                with open(log_file_path, "r") as f:
                    printing = False
                    unity_error = '\n'
                    for l in f:
                        l=l.strip()
                        if (l == 'Exception') or (l=='Error'):
                            printing = True
                            unity_error += '----------------------\n'
                        if (l == ''):
                            printing = False
                        if printing:
                            unity_error += l + '\n'
                    logger.info(unity_error)
                    logger.error("An error might have occured in the environment. "
                        "You can check the logfile for more information at {}".format(log_file_path))
            except:
                logger.error("An error might have occured in the environment. "
               "No unity-environment.log file could be found.") 
        super(UnityTimeOutException, self).__init__(message)

